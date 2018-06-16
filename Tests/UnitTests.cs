using System;
using System.Linq;
using System.Threading;
using Woof.DebugEx;
using Xunit;
using Xunit.Abstractions;

public class UnitTests {

    private readonly ITestOutputHelper Output;

    public UnitTests(ITestOutputHelper output) => Output = output;

    [Fact]
    public void BenchmarkCompareTest() {
        void a() => Thread.Sleep(10);
        void b() => Thread.Sleep(20);
        var a2b = Benchmark.Compare(a, b, 0.1);
        var b2a = Benchmark.Compare(b, a, 0.1);
        Output.WriteLine($"a2b = {a2b}");
        Output.WriteLine($"b2a = {b2a}");
        Assert.True(a2b > 90 && a2b < 110);
        Assert.True(b2a > -60 && b2a < -40);
    }

    [Fact]
    public void BenchmarkPerfTest() {
        void a() => Thread.Sleep(10);
        var result = Benchmark.Perf(a, 0.1);
        Output.WriteLine($"result = {result}");
        Assert.True(result > 90 && result < 110);
    }

    [Fact]
    public void BenchmarkRaceTest() {
        void a() => Thread.Sleep(1);
        void b() => Thread.Sleep(10);
        void c() => Thread.Sleep(100);
        var results = Benchmark.Race(1, a, b, c);
        Output.WriteLine("results = {" + String.Join(", ", results.Select(i => i.ToString())) + "}");
        Assert.True(results[0] > 500);
        Assert.True(results[1] >= 90);
        Assert.True(results[2] >= 9);
    }

    [Fact]
    public void FreezeTestTest() {
        void a() => Thread.Sleep(100);
        new FreezeTest(a).Test(200);
        var isFreezeDetected = false;
        try {
            new FreezeTest(a).Test(50);
        } catch (TimeoutException) {
            isFreezeDetected = true;
        }
        Assert.True(isFreezeDetected);
    }

    [Fact]
    public void StringExtensionsTest() {
        var s1 = "test\r\n\t  test";
        var s2 = s1.WhitespaceVisible();
        Output.WriteLine(s2);
        Assert.Equal("test←↓→··test", s2);
    }

}