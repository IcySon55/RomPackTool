using FluentFTP;
using Microsoft.Data.Sqlite;
using RomPackTool.Core.Processing;
using RomPackTool.Core.Sony;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace RomPackTool.Core.Processes.Vita
{
    /// <summary>
    /// 
    /// </summary>
    public class VitaFtpGetAppList : VitaFtpProcess
    {
        /// <summary>
        /// 
        /// </summary>
        private const string _titleIdPattern = @"P[A-Z]{3}[0-9]{5}";

        /// <summary>
        /// 
        /// </summary>
        private List<string> _titleIdBlacklist = new()
        {
            "PCSI00011"
        };

        /// <summary>
        /// 
        /// </summary>
        private string _cacheDirectory;

        /// <summary>
        /// 
        /// </summary>
        public List<VitaAppEntry> Apps { get; set; } = new();

        /// <summary>
        /// Instantiates a new <see cref="VitaFtpCartToNoNpDrm"/> process.
        /// </summary>
        /// <param name="ipAddress">Vita IP address.</param>
        /// <param name="outputPath">Path to create the dump in.</param>
        public VitaFtpGetAppList(VitaFtpOptions options, string cacheDirectory) : base(options)
        {
            Name = "Refresh Vita App List";
            _cacheDirectory = cacheDirectory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public override async Task<bool> Run(CancellationToken token)
        {
            State = ProcessState.Running;

            try
            {
                // Parameter validation.
                var valid = await Validate(token);
                if (!valid) return false;

                Report($"Connecting to your Vita at {_ipAddress}:1337...");
                await Task.Delay(1, token);

                // Create the FTP client.
                var client = new FtpClient($"ftp://{_ipAddress}", 1337, new NetworkCredential(string.Empty, string.Empty))
                {
                    DataConnectionType = FtpDataConnectionType.AutoActive
                };
                await client.ConnectAsync(token);

                // Connected?
                if (client.IsAuthenticated)
                {
                    Report("Connected!");
                    await Task.Delay(1, token);
                }

                Report("Retrieving app list...");
                await Task.Delay(1, token);

                // Get known title Ids.
                var knownTitleIds = (await client.GetDirectoriesAsync("/ur0:/appmeta", token)).Where(dir =>
                    Regex.IsMatch(dir.Name, _titleIdPattern)
                ).Select(dir => dir.Name).Except(_titleIdBlacklist).ToList();
                knownTitleIds.Sort();

                // Get installed title Ids.
                var installedTitleIds = (await client.GetDirectoriesAsync("/ux0:/app", token)).Where(dir =>
                    Regex.IsMatch(dir.Name, _titleIdPattern)
                ).Select(dir => dir.Name).Except(_titleIdBlacklist).ToList();
                installedTitleIds.Sort();

                // Get cart title Id.
                var cartTitleId = (await client.GetDirectoriesAsync("/gro0:/app", token: token)).Select(dir => dir.Name).FirstOrDefault();

                var cartParamSfoStream = new MemoryStream();
                await client.DownloadAsync(cartParamSfoStream, $"/gro0:/app/{cartTitleId}/sce_sys/param.sfo", token: token);
                var cartParamSfo = new SfoFile(cartParamSfoStream);

                var cartApp = new VitaAppEntry();

                // Update icon cache.
                for (int i = 0; i < knownTitleIds.Count; i++)
                {
                    var titleId = knownTitleIds[i];
                    var iconCachePath = Path.Combine(_cacheDirectory, titleId);
                    var iconFilePath = Path.Combine(iconCachePath, $"icon0.png");
                    var iconThumbFilePath = Path.Combine(iconCachePath, $"icon0_thumb.png");

                    Directory.CreateDirectory(iconCachePath);

                    var appMetaTitlePath = $"/ur0:/appmeta/{titleId}";
                    if (!File.Exists(iconFilePath) || !File.Exists(iconThumbFilePath))
                        if (await client.DirectoryExistsAsync(appMetaTitlePath, token))
                        {
                            var iconPath = $"{appMetaTitlePath}/icon0.png";
                            var ms = new MemoryStream();
                            await client.DownloadAsync(ms, iconPath, token: token);

                            using var img = new Bitmap(ms);
                            img.Save(iconFilePath, ImageFormat.Png);

                            using var thumb = img.GetThumbnailImageHQ(50, 50);
                            thumb.Save(iconThumbFilePath, ImageFormat.Png);
                        }
                }

                // Download app.db
                var appDbPath = "/ur0:/shell/db/app.db";
                var appDbFilePath = Path.Combine(_cacheDirectory, "System", "app.db");
                await client.DownloadFileAsync(appDbFilePath, appDbPath, FtpLocalExists.Overwrite, FtpVerify.None, null, token);

                // Update the database
                var connection = new SqliteConnection($"Data Source={appDbFilePath};");
                connection.Open();

                var selectAppInfo = @$"
SELECT
	ai.titleId AS TitleId,
	(SELECT val FROM tbl_appinfo WHERE titleId = ai.titleId AND key = 572932585) AS FullName,
	(SELECT val FROM tbl_appinfo WHERE titleId = ai.titleId AND key = 4007071428) AS ShortName,
	(SELECT val FROM tbl_appinfo WHERE titleId = ai.titleId AND key = 1299181514) AS ContentId,
	(SELECT val FROM tbl_appinfo WHERE titleId = ai.titleId AND key = 1203824812) AS MininumFirmwareVersion,
	(SELECT val FROM tbl_appinfo WHERE titleId = ai.titleId AND key = 3168212510) AS UpdateVersion,
	(SELECT val FROM tbl_appinfo WHERE titleId = ai.titleId AND key = 3552295351) AS BaseVersion,
	(SELECT val FROM tbl_appinfo WHERE titleId = ai.titleId AND key = 2722548093) AS InstallDate,
	(SELECT val FROM tbl_appinfo WHERE titleId = ai.titleId AND key = 414472871) AS LastUpdateDate,
	(SELECT val FROM tbl_appinfo WHERE titleId = ai.titleId AND key = 1281097414) AS LastPlayedDate
FROM tbl_appinfo ai
WHERE titleId LIKE 'PCS%' AND titleId NOT IN ('{string.Join("','", _titleIdBlacklist)}')
GROUP BY ai.titleId";

                var cmd = new SqliteCommand(selectAppInfo, connection);
                var rows = cmd.ExecuteReader();
                var columns = await rows.GetColumnSchemaAsync(token);

                var appInfo = new DataTable();
                appInfo.Columns.AddRange(columns.Select(c => new DataColumn(c.ColumnName, c.DataType)).ToArray());

                // Build Rows
                while (rows.Read())
                {
                    var dataRow = appInfo.NewRow();
                    foreach (var col in columns.Take(columns.Count))
                        dataRow[col.ColumnName] = rows.GetValue(rows.GetOrdinal(col.ColumnName));
                    appInfo.Rows.Add(dataRow);
                }
                connection.Close();

                // Check the cartridge slot
                await client.SetWorkingDirectoryAsync("/gro0:/app", token);

                // Begin reporting
                Report(0, knownTitleIds.Count);

                // Get Apps
                for (int i = 0; i < knownTitleIds.Count; i++)
                {
                    var titleId = knownTitleIds[i];
                    var iconCachePath = Path.Combine(_cacheDirectory, titleId);
                    var iconThumbFilePath = Path.Combine(iconCachePath, $"icon0_thumb.png");

                    // Create the app entry.
                    var app = new VitaAppEntry { TitleId = titleId, Inserted = titleId == cartTitleId };
                    var dataRow = appInfo.Select($"titleId = '{titleId}'").FirstOrDefault();

                    // Load the icon from cache.
                    if (File.Exists(iconThumbFilePath))
                        app.Icon = Image.FromFile(iconThumbFilePath);

                    // Load the game name
                    app.Name = dataRow?["FullName"]?.ToString();

                    // Load the title info from SFO
                    if (await client.DirectoryExistsAsync($"/ux0:/license/app/{titleId}", token))
                    {
                        if (await client.ThereBeFileAsync($"/ux0:/license/app/{titleId}/6488b73b912a753a492e2714e9b38bc7.rif", token))
                            app.Source = "Memory Card (NoNpDrm)";
                        else
                            app.Source = "Memory Card (PSN)";
                    }
                    else // Catridge app
                        app.Source = app.Inserted ? "Cartridge (Inserted)" : "Cartridge";

                    Apps.Add(app);

                    // Progress updates
                    Report($"Found {dataRow?["FullName"]}...");
                    Report(i + 1);
                    await Task.Delay(1, token);
                }

                await client.DisconnectAsync(token);

                State = ProcessState.Completed;
                return true;
            }
            catch (TaskCanceledException)
            {
                State = ProcessState.Cancelled;
                return false;
            }
            catch (Exception ex)
            {
                State = ProcessState.Error;
                Report(ex.Message);
                return false;
            }
        }
    }
}
