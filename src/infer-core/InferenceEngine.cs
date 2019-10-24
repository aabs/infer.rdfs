using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Update;

namespace infer_core
{
    /// <summary>
    /// Inference engine that works to infer new triples by running SPARQL queries against an existing graph
    /// </summary>
    /// <remarks>
    /// <para>Refer to <a href="https://www.w3.org/TR/rdf11-mt/#patterns-of-rdfs-entailment-informative">here</a> for more details about the inference rules</para>
    /// </remarks>
    public class InferenceEngine : BaseInferenceEngine, IInferenceEngine
    {
        private readonly ISparqlUpdateProcessor _updateProcessor;

        public InferenceEngine(ISparqlUpdateProcessor updateProcessor)
        {
            this._updateProcessor = updateProcessor;
        }

        public void Infer(Uri _, EntailmentRegime entailmentRegime = EntailmentRegime.RDFS)
        {
            foreach (var rule in GetRulesForRegime(entailmentRegime, GetType().Assembly))
            {
                if (!string.IsNullOrWhiteSpace(rule))
                {
                    var sps = new SparqlParameterizedString(rule);
                    var parser = new SparqlUpdateParser();
                    var query = parser.ParseFromString(sps);
                    query.Process(_updateProcessor);
                }
            }
        }


    }

    public abstract class BaseInferenceEngine
    {
        public IEnumerable<string> GetRulesForRegime(EntailmentRegime entailmentRegime, Assembly asm)
        {
            List<string> resources = new List<string>(asm.GetManifestResourceNames());
            resources.Sort();
            foreach (var ruleFile in resources)
            {
                switch (entailmentRegime)
                {
                    case EntailmentRegime.RDFS when ruleFile.Contains("rdfs_rules"):
                        yield return ReadResource(ruleFile, asm);
                        break;
                    case EntailmentRegime.RDFSPLUS when ruleFile.Contains("rdfs_rules") || ruleFile.Contains("owl2_equality"):
                        yield return ReadResource(ruleFile, asm);
                        break;
                    default:
                        break;
                }
            }
        }

        protected string ReadResource(string path, Assembly assembly)
        {
            using var reader = new StreamReader(assembly.GetManifestResourceStream(path));
            return reader.ReadToEnd();
        }    }
}