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
//BenchmarkRunner.Run<ListVsDictionaryTest>();

/*
|                    Method |               value |                pair1 |                pair2 |                pair3 |         formatValues |       Mean |    Error |    StdDev |     Median | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|-------------------------- |-------------------- |--------------------- |--------------------- |--------------------- |--------------------- |-----------:|---------:|----------:|-----------:|------:|--------:|-------:|----------:|------------:|
|   ApplyFormat_WithReplace | Your(...)rk}. [160] | Local(...)cPair [31] | Local(...)cPair [31] | Local(...)cPair [31] |                    ? |   263.7 ns | 15.04 ns |  44.35 ns |   286.5 ns |     ? |       ? | 0.0880 |    1104 B |           ? |
| ApplyFormat_PreparedPairs | Your(...)rk}. [160] | Local(...)cPair [31] | Local(...)cPair [31] | Local(...)cPair [31] |                    ? |   195.4 ns | 15.87 ns |  46.78 ns |   221.0 ns |     ? |       ? | 0.0732 |     920 B |           ? |
| ApplyFormat_StringBuilder | Your(...)rk}. [160] | Local(...)cPair [31] | Local(...)cPair [31] | Local(...)cPair [31] |                    ? | 2,293.3 ns | 45.72 ns | 123.60 ns | 2,333.3 ns |     ? |       ? | 0.0534 |     680 B |           ? |
|                           |                     |                      |                      |                      |                      |            |          |           |            |       |         |        |           |             
|
|               ApplyFormat | Your(...)rk}. [160] |                    ? |                    ? |                    ? | Syste(...)ring] [68] |   751.6 ns | 63.87 ns | 188.32 ns |   797.1 ns |  1.00 |    0.00 | 0.1211 |    1528 B |        1.00 |
 */
//BenchmarkRunner.Run<LocalizationManagerTest>();


/*
|  Method |                  key |      Mean |     Error |    StdDev |   Gen0 | Allocated |
|-------- |--------------------- |----------:|----------:|----------:|-------:|----------:|
|  Concat |           0123456790 | 20.823 ns | 0.1210 ns | 0.1072 ns | 0.0076 |      96 B |
| Builder |           0123456790 | 11.478 ns | 0.1178 ns | 0.1044 ns | 0.0038 |      48 B |
|  Concat | AReal(...)gLong [25] | 20.826 ns | 0.1449 ns | 0.1284 ns | 0.0102 |     128 B |
| Builder | AReal(...)gLong [25] | 12.537 ns | 0.2099 ns | 0.1860 ns | 0.0064 |      80 B |
|  Concat |                   id | 19.711 ns | 0.1471 ns | 0.1228 ns | 0.0063 |      80 B |
| Builder |                   id |  9.587 ns | 0.1110 ns | 0.1039 ns | 0.0025 |      32 B |
 */
BenchmarkRunner.Run<ConcatTest>();