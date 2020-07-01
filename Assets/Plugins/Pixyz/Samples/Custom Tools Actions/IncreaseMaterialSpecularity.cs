using System.Collections.Generic;
using UnityEngine;
using Pixyz.Tools;

namespace Pixyz.Samples
{
    public class IncreaseGlossiness : ActionInOut<IList<Material>, IList<Material>>
    {
        [UserParameter]
        public float increment = 0.1f;

        public override int id { get { return 13457840; } }
        public override string menuPathRuleEngine { get { return "Custom/Increase Glossiness"; } }
        public override string menuPathToolbox { get { return "Custom/Increase Glossiness"; } }
        public override string tooltip { get { return "Increase Material Glossiness"; } }

        public override IList<Material> run(IList<Material> input)
        {
            foreach (Material material in input) {
                material.SetFloat("_Glossiness", Mathf.Clamp(material.GetFloat("_Glossiness") + increment, 0f, 1f));
            }
            return input;
        }
    }
}