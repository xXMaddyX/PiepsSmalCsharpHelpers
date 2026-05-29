using System.Diagnostics;

List<ProcessInformation> ProcessDataList = [];

Process[] AllProcesses = Process.GetProcesses();

foreach (var item in AllProcesses) {
    ProcessDataList.Add(GetProcessInfo(item));
}

var StopCounter = 0;
var Step = 10;
for (var i = 0; i < ProcessDataList.Count; i++) {
    ProcessDataList[i].PrintProcessInformation();
    if (i >= StopCounter + Step) { 
        StopCounter += Step;
        Console.WriteLine("##################################################################");
        Console.WriteLine("-----------------------PRESS_ENTER_TO_CONTINOU--------------------");
        Console.WriteLine("##################################################################");
        Console.ReadLine();
    }
}

Console.WriteLine("Press Enter to Close");
Console.ReadLine();
Console.Clear();

ProcessInformation GetProcessInfo(Process proc) {
    try {
        proc.Refresh();
        return new ProcessInformation() {
            ProcessName = proc.ProcessName,
            ProcessID = proc.Id,
            ProcessRespoding = proc.Responding,
            CurrentUsedMemory = ParseNumberToString(proc.WorkingSet64 / 1024),
        };
    } catch {
        return new ProcessInformation() {
            ProcessName = proc.ProcessName,
            ProcessID = proc.Id,
            ProcessRespoding = false,
            CurrentUsedMemory = null,
        };
    }
}

string ParseNumberToString(long MemoryAsBytes) {
    var MemoryNumber = (decimal)MemoryAsBytes;
    var RAWSTRING = MemoryNumber.ToString();
    var ParsedString = "";
    
    var FirstStringPart = "";
    var Seperator = ".";
    var StringEnd = "";

    for (var i = RAWSTRING.Length - 1; i >= 0; i--) {
        if (i >= RAWSTRING.Length - 4) { StringEnd += RAWSTRING[i]; }
        if (i < RAWSTRING.Length - 4) { FirstStringPart += RAWSTRING[i]; }
    }
    if (FirstStringPart == "") { FirstStringPart = "0"; }
    ParsedString = new string([.. FirstStringPart.Reverse()]) + Seperator + new string([.. StringEnd.Reverse()]);
    return ParsedString;
}

public class ProcessInformation{
    public string? ProcessName { get; set; } = null;
    public bool? ProcessRespoding { get; set; } = null;
    public int? ProcessID { get; set; } = null;
    public string? CurrentUsedMemory { get; set; } = null;

    public void PrintProcessInformation() {
        Console.WriteLine("------------------------------------------------------------------");
        Console.WriteLine($"Process Name: {ProcessName}");
        Console.WriteLine($"Process Responding: {ProcessRespoding}");
        Console.WriteLine($"ProcessID: {ProcessID}");
        Console.WriteLine($"Process Memory KB: {CurrentUsedMemory}");
        Console.WriteLine("------------------------------------------------------------------");
    }
}