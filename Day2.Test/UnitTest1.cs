using static Day2.Utils;

namespace Day2.Test;

public class Tests
{
    [Test]
    [TestCase(new int[] {7,6,4,2,1}, true)]
    [TestCase(new int[] {1,2,7,8,9}, false)]
    [TestCase(new int[] {9,7,6,2,1}, false)]
    [TestCase(new int[] {1,3,2,4,5}, false)]
    [TestCase(new int[] {8,6,4,4,1}, false)]
    [TestCase(new int[] {1,3,6,7,9}, true)]
    public void PartOneSamples(int[] report, bool result) {
        Assert.That(ReportSafety(report), Is.EqualTo(result), "");
    }

    [Test]
    [TestCase(new int[] {7,6,4,2,1}, true)]
    [TestCase(new int[] {1,2,7,8,9}, false)]
    [TestCase(new int[] {9,7,6,2,1}, false)]
    [TestCase(new int[] {1,3,2,4,5}, true)]
    [TestCase(new int[] {8,6,4,4,1}, true)]
    [TestCase(new int[] {1,3,6,7,9}, true)]
    public void PartTwoSamples(int[] report, bool result) {
        Assert.That(ReportSafetyWithDamper(report), Is.EqualTo(result), "");
    }
    [Test]
    [TestCase(new int[] {6,8,11,12,14,16,18,16}, true)]
    [TestCase(new int[] {73,76,79,80,81,84,86,86}, true)]
    [TestCase(new int[] {9,11,13,14,17,24}, true)]
    [TestCase(new int[] {59,61,64,62,65}, true)]
    [TestCase(new int[] {34,35,42,43,44,51}, false)]
    [TestCase(new int[] {80,79,80,81,84,86,88,88}, false)]
    [TestCase(new int[] {14,12,13,14,17,18,21,28}, false)]
    [TestCase(new int[] {55,52,49,52,55,58,59,63}, false)]
    [TestCase(new int[] {39,37,44,46,49,54}, false)]
    [TestCase(new int[] {31,31,32,33,36,39,40,42}, true)]
    [TestCase(new int[] {6,6,7,10,11,14,16,14}, false)]
    [TestCase(new int[] {81,78,76,74,73,71,73}, true)]
    [TestCase(new int[] {78,75,72,71,70,67,67}, true)]
    [TestCase(new int[] {27,25,22,24,24}, false)]
    [TestCase(new int[] {95,93,90,93,87}, true)]
    [TestCase(new int[] {79,77,75,74,71,71,71}, false)]
    [TestCase(new int[] {36,34,34,32,27}, false)]
    [TestCase(new int[] {53,54,56,55,58}, true)]
    [TestCase(new int[] {81,82,77,75,73,70}, false)]
    [TestCase(new int[] {67,67,66,64,63,60}, true)]
    public void PartTwoOthers(int[] report, bool result) {
        Assert.That(ReportSafetyWithDamper(report), Is.EqualTo(result), "");
    }
    [Test]
    [TestCase(new int[] {12,10,11,14,17,18,20,21}, true)]
    [TestCase(new int[] {24,25,24,22,21,18,15,13}, true)]
    [TestCase(new int[] {50,48,49,50,53,56}, true)]
    [TestCase(new int[] {56,58,57,55,54,52}, true)]
    [TestCase(new int[] {91,92,88,86,85,82,81}, true)]
    [TestCase(new int[] {19,22,20,19,18}, true)]
    public void PartTwoMostTricky(int[] report, bool result) {
        Assert.That(ReportSafetyWithDamper(report), Is.EqualTo(result), "");
    }
}
