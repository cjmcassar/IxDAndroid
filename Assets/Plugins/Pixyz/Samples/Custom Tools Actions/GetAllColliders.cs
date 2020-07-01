using System.Collections.Generic;
using UnityEngine;
using Pixyz.Tools;

namespace PiXYZ.Samples
{
    public class GetAllColliders : ActionOut<IList<Collider>>
    {
        [UserParameter]
        public float aFloatParameter = 1f;

        public override int id { get { return 15420328; } }
        public override string menuPathRuleEngine { get { return "Custom/Get All Colliders"; } }
        public override string menuPathToolbox { get { return "Custom/Get All Colliders"; } }
        public override string tooltip { get { return "Get All Colliders"; } }

        public override IList<Collider> run()
        {
            return GameObject.FindObjectsOfType<Collider>();
        }
    }
}