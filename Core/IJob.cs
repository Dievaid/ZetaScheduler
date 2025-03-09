namespace ZetaScheduler.Core
{
    public interface IJob
    {
        /// <summary>
        /// Executes the implementation of the job.
        /// </summary>
        void Execute();
    }
}
