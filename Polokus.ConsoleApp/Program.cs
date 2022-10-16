using Polokus.ConsoleApp.ManualTests;
using Polokus.Core;


List<ManualTest> tests = new()
{
    new ManualTest("manualTask1.bpmn"),
    new ManualTest("message1.bpmn","Use curl localhost:8080/Waiter_(Test)_(Process_0rrvqcp)_(Event_15d655d)"),

};

int successedTests = 0;
for (int i=1; i<= tests.Count; i++)
{
    bool success = await tests[i-1].RunTest(i);
    if (success) successedTests++;
}

Console.WriteLine($"Testing finished. Passed tests: {successedTests}/{tests.Count}");

Console.ReadLine();