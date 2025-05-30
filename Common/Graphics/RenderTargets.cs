﻿using EbonianMod.Common.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace EbonianMod.Common.Graphics;
public static class RTHandler
{
    public static PixelationTarget pixelationTarget => GetInstance<PixelationTarget>();
    public static InvisibleTarget invisTarget => GetInstance<InvisibleTarget>();
    public static GarbageTarget garbageTarget => GetInstance<GarbageTarget>();
    public static XareusTarget xareusTarget => GetInstance<XareusTarget>();
}
public class PixelationTarget : CommonRenderTarget
{
    public override void HandleUseRequest(GraphicsDevice gd, SpriteBatch sb)
    {
        PrepareAndSet(ref _target2, gd);
        sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        EbonianMod.pixelationDrawCache.InvokeAllAndClear();
        EbonianMod.primitivePixelationDrawCache.InvokeAllAndClear();
        sb.End();

        PrepareAndSet(ref _target, gd);
        sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, EbonianMod.colorQuant.Value, Matrix.Identity);
        EbonianMod.colorQuant.Value.Parameters["res"].SetValue(32);
        sb.Draw(_target2, new Rectangle(0, 0, (int)(Main.screenWidth / (1 + Main.GameZoomTarget)), (int)(Main.screenHeight / (1 + Main.GameZoomTarget))), Color.White);
        sb.End();
    }
}
public sealed class InvisibleTarget : CommonRenderTarget// if he's invisible how did he die
{
    public override void HandleUseRequest(GraphicsDevice gd, SpriteBatch sb)
    {
        PrepareAndSet(ref _target2, gd);
        sb.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        EbonianMod.affectedByInvisibleMaskCache.InvokeAllAndClear();
        sb.End();

        PrepareAndSet(ref _target, gd);
        sb.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        EbonianMod.invisibleMaskCache.InvokeAllAndClear();
        sb.End();
    }
}
public sealed class GarbageTarget : CommonRenderTarget
{
    public override void HandleUseRequest(GraphicsDevice gd, SpriteBatch sb)
    {
        PrepareAndSet(ref _target, gd);
        sb.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        EbonianMod.garbageFlameCache.InvokeAllAndClear();
        sb.End();
    }
}
public sealed class XareusTarget : CommonRenderTarget
{
    public override void HandleUseRequest(GraphicsDevice gd, SpriteBatch sb)
    {
        PrepareAndSet(ref _target, gd);
        sb.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        EbonianMod.xareusGoopCache.InvokeAllAndClear();
        sb.End();
    }
}
