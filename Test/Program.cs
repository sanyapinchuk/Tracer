using Tracer;
using Serializer;
using Test.Interfaces;
using Test.SerializationResultWrites;
using Serializer.Interfaces;
using Serializer.Json;
using Serializer.Xml;

var tracer = new Tracer.Tracer();
var temp = new Foo(tracer);

temp.MyMethod();

var arrayThreads = tracer.GetTraceResult();


//write results 

ITraceResultSerializer<List<ThreadInfo>> serializer = new Serializer.Json.JsonSerializer();
var json = serializer.Serialize(arrayThreads);

serializer = new Serializer.Xml.XmlSerializer();
var xml = serializer.Serialize(arrayThreads);


ISerializationResultWriter writer = new SerializationResultConsoleWriter();
writer.Write(json);
writer.Write(xml);


writer = new SerializationResultFileWriter("trace.xml");
writer.Write(xml);
((SerializationResultFileWriter)writer).Path = "trace.json";
writer.Write(json);


public class Foo
{
    private Bar _bar;
    private ITracer _tracer;

    internal Foo(ITracer tracer)
    {
        _tracer = tracer;
        _bar = new Bar(_tracer);
    }

    public void MyMethod()
    {
        _tracer.StartTrace();
        for (int i = 0; i < 1000; i++)
        {
            i++;
        }
        _bar.InnerMethod();

        using (var sw = new FileStream("../../tempFile.txt", FileMode.Create))
        {
            for (byte i = 0; i < 200; i++)
            {
                sw.WriteByte(i);
            }
        }
        
        _tracer.StopTrace();
    }
}

public class Bar
{
    private ITracer _tracer;

    internal Bar(ITracer tracer)
    {
        _tracer = tracer;
    }

    public void InnerMethod()
    {
        _tracer.StartTrace();
        int[] arr = new int[1000];
        Array.Sort(arr);
        _tracer.StopTrace();
    }
}

