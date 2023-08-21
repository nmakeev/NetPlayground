using BenchmarkDotNet.Running;
using NetPlayground.Tests;

/*
|               Method |      Mean |    Error |   StdDev |
|--------------------- |----------:|---------:|---------:|
|      SumWithForClass |  97.42 us | 1.914 us | 3.548 us |
|     SumWithForStruct |  89.97 us | 0.273 us | 0.242 us |
|  SumWithForeachClass |  93.18 us | 1.485 us | 1.240 us |
| SumWithForeachStruct | 155.12 us | 0.335 us | 0.297 us |
|     SumWithLinqClass | 991.13 us | 5.919 us | 5.537 us |
|    SumWithLinqStruct | 874.54 us | 0.894 us | 0.698 us |
 */
//BenchmarkRunner.Run<EnumerationTests>();

/*
|               Method |       Mean |     Error |    StdDev |
|--------------------- |-----------:|----------:|----------:|
|      GetFirstByIndex |  0.2317 ns | 0.0151 ns | 0.0141 ns |
| GetFirstByEnumerator | 12.1287 ns | 0.2712 ns | 0.3330 ns |
|       GetFirstByLinq |  8.3590 ns | 0.0363 ns | 0.0303 ns | 
 */
//BenchmarkRunner.Run<FirstOrDefaultTests>();

/*
|                        Method |          Mean |         Error |        StdDev |
|------------------------------ |--------------:|--------------:|--------------:|
|                SumWithNewList |      38.35 us |      0.207 us |      0.183 us |
|           SumWithListFromPool | 157,796.15 us | 15,340.665 us | 45,232.296 us |
| SumWithListFromPoolTryFinally | 157,647.85 us | 15,877.483 us | 46,815.117 us |
 */
//BenchmarkRunner.Run<NaivePoolTests>();

/*
|         Method |      Mean |     Error |    StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------------- |----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| GenerateRandom |  8.501 ns | 0.0838 ns | 0.0784 ns |  1.00 |    0.00 |      - |         - |          NA |
|   TestToString | 29.426 ns | 0.2643 ns | 0.2473 ns |  3.46 |    0.03 | 0.0019 |      24 B |          NA |
|    TestConvert | 18.026 ns | 0.1717 ns | 0.1522 ns |  2.12 |    0.02 |      - |         - |          NA | 
 */
//BenchmarkRunner.Run<EnumStringVsBoxTests>();

/*
|               Method |                 dict |                 list |        Mean |      Error |     StdDev |      Median | Allocated |
|--------------------- |--------------------- |--------------------- |------------:|-----------:|-----------:|------------:|----------:|
| SelectFromDictionary | Syste(...)Item] [78] |                    ? |    52.60 ns |   1.089 ns |   3.211 ns |    53.45 ns |         - |
| SelectFromDictionary | Syste(...)Item] [78] |                    ? |   133.47 ns |   5.535 ns |  16.321 ns |   140.83 ns |         - |
| SelectFromDictionary | Syste(...)Item] [78] |                    ? |   169.81 ns |  19.604 ns |  57.804 ns |   137.16 ns |         - |
| SelectFromDictionary | Syste(...)Item] [78] |                    ? |   260.37 ns |   5.149 ns |   7.218 ns |   259.81 ns |         - |
|       SelectFromList |                    ? | Syste(...)Item] [59] |    65.83 ns |   3.057 ns |   9.014 ns |    69.64 ns |         - |
|       SelectFromList |                    ? | Syste(...)Item] [59] |   330.12 ns |   7.816 ns |  23.046 ns |   338.38 ns |         - |
|       SelectFromList |                    ? | Syste(...)Item] [59] | 1,304.24 ns |  25.749 ns |  51.424 ns | 1,315.20 ns |         - |
|       SelectFromList |                    ? | Syste(...)Item] [59] | 5,051.22 ns | 130.427 ns | 384.568 ns | 5,244.63 ns |         - |
 */
BenchmarkRunner.Run<ListVsDictionaryTest>();