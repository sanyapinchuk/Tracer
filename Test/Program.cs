using Tracer;
using Serializer;

var tracer = new Tracer.Tracer();
var temp = new Foo(tracer);

temp.MyMethod();

var arrayThreads = tracer.GetTraceResult();
var str = Serializer.XmlSerializer.Serialize(arrayThreads);

Console.WriteLine(str);

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

