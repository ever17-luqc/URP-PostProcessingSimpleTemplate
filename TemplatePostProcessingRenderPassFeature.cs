


using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TemplatePostProcessingRenderPassFeature : ScriptableRendererFeature
{

    #region MaterialLiberary类
    //-----------------材质往这里加-------------
    [Serializable]
    public class MaterialLiberary
    {
        public Material tempMaterial = null;

    }
    public MaterialLiberary materialLiberary = new MaterialLiberary();
    #endregion
    

    CustomRenderPass m_ScriptablePass;
   
    public override void Create()
    {
        m_ScriptablePass = new CustomRenderPass();




    }


    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        m_ScriptablePass.SetUp(renderer.cameraColorTarget, materialLiberary);
        m_ScriptablePass.renderPassEvent = RenderPassEvent.AfterRenderingTransparents;



        renderer.EnqueuePass(m_ScriptablePass);
    }


    //------------------------------------------==========================================================================================
    #region renderpass
    class CustomRenderPass : ScriptableRenderPass
    {
        const string template_Tag = "Template Passb Tag";


        //--------------------------------------------------
        TemplateEffect templateEffect;
        RenderTargetIdentifier sourceTarget;
        
       

        MaterialLiberary materialLiberary;
        public CustomRenderPass()
        {

            renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
             
        }
        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
           
        }
        public void SetUp(RenderTargetIdentifier sourceTarget, MaterialLiberary materialLiberary)
        {
            this.sourceTarget = sourceTarget;
            this.materialLiberary = materialLiberary;


        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {

            //-------------------------------------------------//
            var stack = VolumeManager.instance.stack;
            templateEffect = stack.GetComponent<TemplateEffect>();
            //-------------------------------------------------//
            //======================TemplateEffect  
            if (templateEffect.IsActive())
            {


                DoTemplateEffect(context, ref renderingData, materialLiberary.tempMaterial, templateEffect);


            }
            //======================TemplateEffect  


        }


        public override void FrameCleanup(CommandBuffer cmd)
        {
        }

        #region TemplateEffect
        void DoTemplateEffect(ScriptableRenderContext context, ref RenderingData renderingData,Material templateMaterial,TemplateEffect templateEffect)
        {
            
            CommandBuffer cmd = CommandBufferPool.Get(template_Tag);
            using (new ProfilingScope(cmd, new ProfilingSampler(template_Tag))) 
            {   
                
                templateMaterial.SetColor("_TemplateColor", templateEffect.templateColor.value);
                
                cmd.Blit(this.sourceTarget, this.sourceTarget, templateMaterial);
                
            }
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);

        }
        #endregion
    }
    #endregion
    //=========================================================================================================


}


