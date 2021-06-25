﻿namespace Ninject.SpecFlowPlugin.Hooks
{
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class NinjectKernelDisposer
    {
        private readonly IKernel scenarioKernel;

        public NinjectKernelDisposer(IKernel scenarioKernel)
        {
            this.scenarioKernel = scenarioKernel;
        }

        // we need to ensure that this is called as late as possible
        // because it can well be that AfterFeature hooks use
        // objects from the kernel
        [AfterFeature(Order = Constants.KernelDisposerOrder)]
        public static void CleanupFeatureDisposables(IKernel featureKernel)
        {
            featureKernel.CheckNullArgument(nameof(featureKernel));

            featureKernel.Dispose();
        }

        // we need to ensure that this is called as late as possible
        // because it can well be that AfterTestRun hooks use
        // objects from the kernel
        [AfterTestRun(Order = Constants.KernelDisposerOrder)]
        public static void CleanupTestThreadDisposables(IKernel testThreadKernel)
        {
            testThreadKernel.CheckNullArgument(nameof(testThreadKernel));

            testThreadKernel.Dispose();
        }

        // we need to ensure that this is called as late as possible
        // because it can well be that AfterScenario hooks use
        // objects from the kernel
        [AfterScenario(Order = Constants.KernelDisposerOrder)]
        public void CleanupScenarioDisposables()
        {
            this.scenarioKernel.Dispose();
        }
    }
}