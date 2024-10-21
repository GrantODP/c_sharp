using Hospital.Collections;
using Hospital.Care;

namespace Hospital.Parser
{


  class TxtParser
  {

    public static HPriorityQueue<Patient> Parse(string filePath)
    {
      var lines = File.ReadLines(filePath);

      var queue = new HPriorityQueue<Patient>();

      foreach (var line in lines)
      {
        queue.Push(ExtractPatientFromLine(line));
      }
      return queue;
    }

    private static Patient ExtractPatientFromLine(string line)
    {
      var patientDetails = line.Split(",");
      Patient p = new Patient();
      p.RegisterNumber = Int32.Parse(patientDetails[0]);
      p.Name = patientDetails[1];
      p.Surname = patientDetails[2];
      p.Priority = (CarePriority)Int32.Parse(patientDetails[3]);
      return p;

    }

  }
}
