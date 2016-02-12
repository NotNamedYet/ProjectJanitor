using System.Collections.Generic;
using System.Text;

public class PrettyCode
{
    public const string SUFFIX = "##"; 
    static List<string> s_cache = new List<string>();
    static char[] s_inputs = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    static System.Random s_random = new System.Random();

    string m_output;

    //Constructor
    public PrettyCode()
    {
        Generate();
    }

    /// <summary>
    /// Generate a code with pattern : [a-zA-Z]{1}[0-9]{4}[a-zA-Z]{1}## Example: a1234Z##
    /// </summary>
    void Generate()
    {
        do
        {
            StringBuilder buffer = new StringBuilder();

            //STARTING CHAR;
            buffer.Append(GenChar());
            
            //NUMERIC INTERCODE
            for (int i = 1; i < 5; i++)
                buffer.Append(s_random.Next(0, 10));

            //ENDING CHAR
            buffer.Append(GenChar());

            //VALIDATOR SUFFIX
            buffer.Append("##");


            m_output = buffer.ToString();

        } while (s_cache.Contains(m_output));

        s_cache.Add(m_output);
    }

    char GenChar()
    {
        char retval = s_inputs[s_random.Next(0, s_inputs.Length - 1)];
        return (s_random.NextDouble() > 0.5 ? retval : char.ToLower(retval));
    }

    public override string ToString()
    {
        return m_output;
    }
}
