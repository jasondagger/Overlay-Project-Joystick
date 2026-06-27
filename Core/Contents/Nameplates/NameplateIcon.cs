
using System.Collections.Generic;
using Godot;
using Overlay.Core.Services.ColorInterpolators;

namespace Overlay.Core.Contents.Nameplates;

public sealed partial class NameplateIcon() :
    ColorRect()
{
    public override void _ExitTree()
    {
        this.RemoveInstance();
    }

    public override void _Process(
        double delta
    )
    {
        this.UpdateShaderResources();
    }
    
    public override void _Ready()
    {
        this.AddInstance();
        this.RetrieveResources();
        this.SetShaderMaterial();
    }

    internal static void SetInitialIconColor(
        ServiceColorInterpolatorColorMode color
    )
    {
        NameplateIcon.s_initialColor = color;
    }

    internal static void UpdateIconColor(
        ServiceColorInterpolatorColorMode color
    )
    {
        foreach (var instance in NameplateIcon.Instances)
        {
            lock (instance.m_lock)
            {
                instance.m_color = color;
            }
        }
    }

    private static readonly List<NameplateIcon>      Instances                        = [];
    private static ServiceColorInterpolatorColorMode s_initialColor                   = ServiceColorInterpolatorColorMode.Transition;
    
    private readonly object                          m_lock                           = new();
    
    private ServiceColorInterpolatorColorMode       m_color                           = ServiceColorInterpolatorColorMode.Transition;
    private ShaderMaterial                          m_material                        = null;
    private ServiceColorInterpolatorInverse         m_serviceColorInterpolatorInverse = null;

    private void AddInstance()
    {
        NameplateIcon.Instances.Add(
            item: this
        );
    }

    private void RemoveInstance()
    {
        NameplateIcon.Instances.Remove(
            item: this
        );
    }
    
    private void RetrieveResources()
    {
        this.m_serviceColorInterpolatorInverse = Services.Services.GetService<ServiceColorInterpolatorInverse>();
    }
    
    private void SetShaderMaterial()
    {
        this.m_material = (ShaderMaterial) this.Get(
            property: $"material"
        );
        this.m_color    = NameplateIcon.s_initialColor;
        
        this.UpdateShaderResources();
    }
    
    private void UpdateShaderResources()
    {
        lock (this.m_lock)
        {
            if (this.m_color is ServiceColorInterpolatorColorMode.Transition)
            {
                var color = this.m_serviceColorInterpolatorInverse.GetColorByCurrentMode(
                    colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0
                );
                this.m_material.SetShaderParameter(
                    param: $"color",
                    value: color
                );
            }
            else
            {
                var color = this.m_serviceColorInterpolatorInverse.GetColorByMode(
                    colorMode:      this.m_color,
                    colorIndexType: IServiceColorInterpolatorDefinition.ColorIndexType.Color0
                );
                this.m_material.SetShaderParameter(
                    param: $"color",
                    value: color
                );
            }
        }
    }
}