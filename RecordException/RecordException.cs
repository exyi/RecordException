using System;
using System.Text;

namespace RecordExceptions
{
public abstract class RecordException : Exception, IEquatable<RecordException>
{
    protected virtual Type EqualityContract => typeof(RecordException);

    public override string Message => Msg ?? GenerateRecordMessage();

    private string? Msg { get; init; }

    public RecordException(string? msg, Exception? InnerException = null): base(msg, InnerException)
    {
        this.Msg = msg;
    }
    public RecordException(Exception? InnerException = null): base(null, InnerException)
    {
    }

    public string GenerateRecordMessage()
    {
        var sb = new StringBuilder();
        PrintMembers(sb);
        return sb.ToString();
    }


    // TODO: make this sealed when they release https://github.com/dotnet/csharplang/issues/4174
    // or maybe not, IDK

    public override string ToString()
    {
        return base.ToString();
    }

    protected virtual bool PrintMembers(StringBuilder builder)
    {

        if (builder.Length == 0)
            return false;

        // another hack: print something reasonable when they override ToString
        builder.Clear();

        builder.AppendLine(base.ToString());

        builder.Append(GetType().Name);
        builder.Append(" properties: { ");

        return false;
    }

    // public static int TestMethod_Bazmek() => 1;

    public static bool operator !=(RecordException left, RecordException right)
    {
        return !(left == right);
    }

    public static bool operator ==(RecordException left, RecordException right)
    {
        if ((object)left != right)
        {
            if ((object)left != null)
            {
                return left.Equals(right);
            }
            return false;
        }
        return true;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as RecordException);
    }

    public virtual bool Equals(RecordException? other)
    {
        return (object?)other == this;
    }

    // renamed to <Clone>$
    public abstract RecordException RecordClone();
    // {
    //     return new RecordException(this);
    // }

    protected RecordException(RecordException original): this(original.Msg, original.InnerException)
    {
    }

    public void Deconstruct(out string Msg, out Exception? InnerException)
    {
        Msg = this.Message;
        InnerException = this.InnerException;
    }
}

}
