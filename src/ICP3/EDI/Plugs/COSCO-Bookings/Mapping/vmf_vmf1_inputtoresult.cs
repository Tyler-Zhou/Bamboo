using System.Collections;

namespace COSCO.Booking
{

    public class vmf1_inputtoresult : Altova.TraceProvider
    {




        class Main : IEnumerable
        {
            string var1_input;

            public Main(
                string var1_input
                )
            {
                this.var1_input = var1_input;
            }

            public IEnumerator GetEnumerator() { return Altova.Mapforce.MFEmptySequence.Instance.GetEnumerator(); }

        }


        public static IEnumerable Create(
            string var1_input
            )
        {

            object o = hash[var1_input];
            if (o != null)
                return new Altova.Mapforce.MFSingletonSequence(o);
            else
                return Altova.Mapforce.MFEmptySequence.Instance;

        }

        static Hashtable hash = new Hashtable();
        static vmf1_inputtoresult()
        {
            hash["1"] = "A";
            hash["5"] = "U";
            hash["9"] = "N";
        }

    } 

}
