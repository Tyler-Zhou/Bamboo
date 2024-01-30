////////////////////////////////////////////////////////////////////////
//
// MappingMapTocs
//
// This file was generated by MapForce 2010.
//
// YOU SHOULD NOT MODIFY THIS FILE, BECAUSE IT WILL BE
// OVERWRITTEN WHEN YOU RE-RUN CODE GENERATION.
//
// Refer to the MapForce Documentation for further details.
// http://www.altova.com/mapforce
//
////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Xml;
using Altova.Xml;

using ICP.EDI.ServiceInterface;


namespace PIL.Booking

{
	
	public class MappingMapToTextfile : Altova.TraceProvider ,IMapping
	{
        private bool runDoesCloseAll = true;
        public bool CloseObjectsAfterRun { get { return runDoesCloseAll; } set { runDoesCloseAll = value; } }




        #region Members
        // instances
        protected XmlNode m_IFCSUM_FYCW2Instance;
        // members
        #endregion //Members


        public void Run(String IFCSUM_FYCW2SourceFilename, string TextfileTargetFilename)
        {
            // open source streams
            WriteTrace("Loading " + IFCSUM_FYCW2SourceFilename + "...\n");
            Altova.IO.Input IFCSUM_FYCW2Source = new Altova.IO.FileInput(IFCSUM_FYCW2SourceFilename);
            // open target stream
            Altova.IO.Output TextfileTarget = new Altova.IO.FileOutput(TextfileTargetFilename);

            // run
            Run(IFCSUM_FYCW2Source, TextfileTarget);

            // close source streams
            IFCSUM_FYCW2Source.Stream.Close();
            // close target stream
            WriteTrace("Saving " + TextfileTargetFilename + "...\n");
            TextfileTarget.Stream.Close();
        }

        public void Run(Altova.IO.Input IFCSUM_FYCW2Source, Altova.IO.Output TextfileTarget)
        {
            // Open the source(s)
            XmlDocument IFCSUM_FYCW2DocSourceObject = XmlTreeOperations.LoadDocument(IFCSUM_FYCW2Source);


            m_IFCSUM_FYCW2Instance = IFCSUM_FYCW2DocSourceObject;
            IFCSUM_FYCW2Source.Close();
            // Create the target
            TextfileDocument TextfileTargetDoc = new TextfileDocument(Textfile_TypeInfo.binder.Types[Textfile_TypeInfo._altova_ti_altova_RowsType]);
            Altova.TextParser.TableLike.Table TextfileTargetObject = TextfileTargetDoc;
            TextfileTargetDoc.Format.AssumeFirstRowAsHeaders = false;
            TextfileTargetDoc.Format.FieldDelimiter = ',';
            TextfileTargetDoc.Format.RemoveEmpty = true;
            TextfileTargetDoc.Format.QuoteCharacter = '\0';

            // Execute mapping

            seq1_Main mapping = new seq1_Main(
new Altova.Mapforce.DOMDocumentNodeAsMFNodeAdapter(m_IFCSUM_FYCW2Instance, IFCSUM_FYCW2Source.Filename));

            Altova.Mapforce.MFTextWriter.Write(mapping, TextfileTargetObject);

            // Close the target

            TextfileTargetDoc.SetEncoding("UTF-8", false, false);
            TextfileTargetDoc.Save(TextfileTarget);

            // Close the Source Library

            if (runDoesCloseAll)
            {
                IFCSUM_FYCW2Source.Close();
                TextfileTarget.Close();
            }
        }
        class seq1_Main : IEnumerable
        {
            Altova.Mapforce.IMFNode var1_instance_IFCSUM_FYCW;

            public seq1_Main(
                Altova.Mapforce.IMFNode var1_instance_IFCSUM_FYCW
                )
            {
                this.var1_instance_IFCSUM_FYCW = var1_instance_IFCSUM_FYCW;
            }

            public IEnumerator GetEnumerator() { return new Enumerator(this); }

            class Enumerator : Altova.Mapforce.IMFEnumerator
            {
                int state = 1;
                object current = null;
                int pos = 0;
                seq1_Main closure;
                string var3_const_http___tempuri_org_I;
                System.Collections.IEnumerable var4_filter_elements;
                IEnumerator var2_map_filter_elements;
                public Enumerator(seq1_Main closure)
                {
                    this.closure = closure;
                }

                public void Reset() { state = 0; pos = 0; }
                public int Position { get { return pos; } }
                public object Current { get { return current; } }

                public bool MoveNext()
                {
                    while (state != 0)
                    {
                        switch (state)
                        {
                            case 1:
                                state = 9;
                                var3_const_http___tempuri_org_I = ("http://tempuri.org/IFCSUM_FYCW.xsd");
                                var4_filter_elements = new Altova.Functions.Core.SequenceCache(Altova.Functions.Core.FilterElements(Altova.Functions.Core.CreateQName("IFCSUM_FYCW", var3_const_http___tempuri_org_I), closure.var1_instance_IFCSUM_FYCW));
                                if (!(Altova.Functions.Core.Exists(var4_filter_elements))) { state = 0; return false; }
                                var2_map_filter_elements = (Altova.Functions.Core.FilterElements(Altova.Functions.Core.CreateQName("EDI", var3_const_http___tempuri_org_I), (Altova.Mapforce.IMFNode)Altova.Functions.Core.First(var4_filter_elements))).GetEnumerator();
                                goto case 9;
                            case 9:
                                state = 9;
                                if (!var2_map_filter_elements.MoveNext()) { state = 10; goto case 10; }
                                current = Altova.Functions.Core.CreateElement(Altova.Functions.Core.CreateQName("Rows", ""), (new seq2_cond_create_element((Altova.Mapforce.IMFNode)(var2_map_filter_elements.Current))));
                                pos++;
                                return true;
                            case 10:
                                state = 0;
                                Altova.Mapforce.MFEnumerator.Dispose(var2_map_filter_elements); var2_map_filter_elements = null;
                                return false;
                        }
                    }
                    return false;
                }

                public void Dispose()
                {
                    Altova.Mapforce.MFEnumerator.Dispose(var2_map_filter_elements); var2_map_filter_elements = null;
                }
            }

        }
        class seq2_cond_create_element : IEnumerable
        {
            Altova.Mapforce.IMFNode var1_cur_filter_elements;

            public seq2_cond_create_element(
                Altova.Mapforce.IMFNode var1_cur_filter_elements
                )
            {
                this.var1_cur_filter_elements = var1_cur_filter_elements;
            }

            public IEnumerator GetEnumerator() { return new Enumerator(this); }

            class Enumerator : Altova.Mapforce.IMFEnumerator
            {
                int state = 1;
                object current = null;
                int pos = 0;
                seq2_cond_create_element closure;
                System.Collections.IEnumerable var2_filter_elements;
                public Enumerator(seq2_cond_create_element closure)
                {
                    this.closure = closure;
                }

                public void Reset() { state = 0; pos = 0; }
                public int Position { get { return pos; } }
                public object Current { get { return current; } }

                public bool MoveNext()
                {
                    while (state != 0)
                    {
                        switch (state)
                        {
                            case 1:
                                state = 0;
                                var2_filter_elements = new Altova.Functions.Core.SequenceCache(Altova.Functions.Core.FilterElements(Altova.Functions.Core.CreateQName("EDIDATA", "http://tempuri.org/IFCSUM_FYCW.xsd"), closure.var1_cur_filter_elements));
                                if (!(Altova.Functions.Core.Exists(var2_filter_elements))) { state = 0; return false; }
                                current = Altova.Functions.Core.CreateElement(Altova.Functions.Core.CreateQName("DATA", ""), Altova.Functions.Core.Box(Altova.CoreTypes.NodeToString((Altova.Mapforce.IMFNode)Altova.Functions.Core.First(var2_filter_elements))));
                                pos++;
                                return true;
                        }
                    }
                    return false;
                }

                public void Dispose()
                {
                }
            }

        }
  } 


}
