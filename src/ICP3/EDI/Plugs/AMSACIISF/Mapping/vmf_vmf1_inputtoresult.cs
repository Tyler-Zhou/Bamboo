////////////////////////////////////////////////////////////////////////
//
// vmf/vmf1_inputtoresult.cs
//
// This file was generated by MapForce 2011r2.
//
// YOU SHOULD NOT MODIFY THIS FILE, BECAUSE IT WILL BE
// OVERWRITTEN WHEN YOU RE-RUN CODE GENERATION.
//
// Refer to the MapForce Documentation for further details.
// http://www.altova.com/mapforce
//
////////////////////////////////////////////////////////////////////////

using System.Collections;


namespace vmf

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
			hash["1"] = "E";	
			hash["2"] = "S";	
			hash["3"] = "C";	
			hash["4"] = "P";	
		}

  } 
}
