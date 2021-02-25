using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace UnityEngine.Rendering.Universal
{
    [Serializable, VolumeComponentMenu("CustomPostProcessing/YourTemplate-PostProcessing")]
    public sealed class TemplateEffect : VolumeComponent, IPostProcessComponent
    {
        public BoolParameter Enable = new BoolParameter(false);
        public ColorParameter templateColor = new ColorParameter(new Color(0, 0, 0));
        public bool IsActive()
        {
            return active&&Enable.value;
        }

        
        public bool IsTileCompatible()
        {
            return false;
        }
       
    }

}