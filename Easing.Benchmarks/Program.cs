using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
using BenchmarkDotNet.Validators;

namespace Depra.Easing.Benchmarks
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var benchmark = BenchmarkSwitcher.FromTypes(new[]
			{
				typeof(EasingBenchmarks),
			});

			IConfig configuration = DefaultConfig.Instance
				.AddDiagnoser(MemoryDiagnoser.Default)
				.AddValidator(JitOptimizationsValidator.FailOnError)
				.AddJob(Job.Default.WithToolchain(InProcessNoEmitToolchain.Instance))
				.WithOptions(ConfigOptions.DisableOptimizationsValidator)
				.WithOrderer(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest));

			if (args.Length > 0)
			{
				benchmark.Run(args, configuration);
			}
			else
			{
				benchmark.RunAll(configuration);
			}
		}
	}
}