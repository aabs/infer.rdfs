using System;

namespace infer_core
{
    public enum EntailmentRegime
    {
        RDFS, OWL2_EL, OWL2_QL, OWL2_RL, OWL2_FULL
    }
    public interface IInferenceEngine
    {
        void Infer(EntailmentRegime entailmentRegime = EntailmentRegime.RDFS);
    }
}
