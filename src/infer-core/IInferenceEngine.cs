using System;

namespace infer_core
{
    public enum EntailmentRegime
    {
        RDFS, RDFSPLUS, OWL2_EL, OWL2_QL, OWL2_RL, OWL2_FULL
    }
    public interface IInferenceEngine
    {
        void Infer(Uri sourceGraphUri, EntailmentRegime entailmentRegime = EntailmentRegime.RDFS);
    }
}
