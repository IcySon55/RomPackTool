using RomPackTool.Core.Processing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RomPackTool.Core.Processes
{
    /// <summary>
    /// Downloads a Vita Cartridge as a NoNpDrm RomPack archive.
    /// </summary>
    public class TestProcess : Process
    {
        /// <summary>
        /// Instantiates the <see cref="TestProcess"/>, for testing~
        /// </summary>
        public TestProcess(IProgress<ProgressReport> progress) : base(progress)
        {
            Name = "Test Process";
            Progress = progress;
        }

        /// <summary>
        /// Always true for the test process.
        /// </summary>
        /// <returns></returns>
        protected override async Task<bool> Validate(CancellationToken token) => true;

        /// <summary>
        /// Executes nothing and moves the progress bar.
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        public override async Task<bool> Run(CancellationToken token)
        {
            // Update state.
            State = ProcessState.Running;

            try
            {
                Progress.Report(new ProgressReport { Value = 0 });

                // Task start pause for effect.
                await Task.Delay(1000, token);

                if (token.IsCancellationRequested)
                {
                    State = ProcessState.Cancelled;
                    return false;
                }

                for (var i = 0; i < 100; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        State = ProcessState.Cancelled;
                        return false;
                    }

                    await Task.Delay(50, token);

                    Progress.Report(new ProgressReport { Value = i + 1 });
                }

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
                Progress.Report(new ProgressReport { Message = ex.Message });
                return false;
            }
        }
    }
}
