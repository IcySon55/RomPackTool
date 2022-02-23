using FluentFTP;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RomPackTool.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class FluentFtpExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="path"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<bool> ThereBeFileAsync(this FtpClient client, string path = null, CancellationToken token = default)
        {
            // Store current working directory to restore it later.
            var previousWorkingDirectory = await client.GetWorkingDirectoryAsync(token);

            // Split paths
            var directory = Path.GetDirectoryName(path);
            var fileName = Path.GetFileName(path);

            // Switch to the target path.
            await client.SetWorkingDirectoryAsync(directory, token);

            // Collect the directories into 
            var files = (await client.GetListingAsync(token)).Where(dir => dir.Type == FtpFileSystemObjectType.File).ToArray();

            // Restore the previous working directory.
            await client.SetWorkingDirectoryAsync(previousWorkingDirectory, token);

            // Return the directories
            return files.Any(f => f.Name == fileName);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="path"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<FtpListItem[]> GetDirectoriesAsync(this FtpClient client, string path = "/", CancellationToken token = default)
        {
            // Store current working directory to restore it later.
            var previousWorkingDirectory = await client.GetWorkingDirectoryAsync(token);

            // Switch to the target path.
            await client.SetWorkingDirectoryAsync(path, token);

            // Collect the directories into 
            var directories = (await client.GetListingAsync(token)).Where(dir => dir.Type == FtpFileSystemObjectType.Directory).ToArray();

            // Restore the previous working directory.
            await client.SetWorkingDirectoryAsync(previousWorkingDirectory, token);

            // Return the directories
            return directories;
        }
    }
}
