using SeparatistCrisis.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SeparatistCrisis.Patches
{
  public class MainMenuPatches
  {
    public MainMenuPatches() => this.SetMenuOptions();

    private void SetMenuOptions()
    {
      List<InitialStateOption> list = Module.CurrentModule.GetInitialStateOptions().ToList<InitialStateOption>();
      Module.CurrentModule.ClearStateOptions();
      foreach (InitialStateOption initialStateOption in list)
      {
        if (initialStateOption.Id.Equals("StoryModeNewGame"))
          Module.CurrentModule.AddInitialStateOption(new InitialStateOption(initialStateOption.Id, initialStateOption.Name, initialStateOption.OrderIndex, (Action) null, (Func<(bool, TextObject)>) (() => (true, new TextObject("Separatist Crisis does not support Story Mode at the moment. You can start Sandbox instead.")))));
        else
          Module.CurrentModule.AddInitialStateOption(initialStateOption);
      }
      Module.CurrentModule.AddInitialStateOption(new InitialStateOption("ScShaderCache", new TextObject("Build Shader Cache"), 4, new Action(this.OnForceClick), (Func<(bool, TextObject)>) (() => (false, new TextObject()))));
    }

    private void OnForceClick() => this.DisplayWindow();

    private void DisplayWindow() => InformationManager.ShowInquiry(new InquiryData("Important warning", "This will load a scene with all the unique troops and NPCs present in our mod. The purpose of this is to compile the local shader cache on your PC.\n \nTHIS WILL TAKE A LONG TIME!!!\nOur users report anything between 20 and 60 minutes.\n \nYou only need to do this once after installing or updating this mod \n \nThis ensures that you won't need to compile the shaders individually during normal gameplay, as it can cause issues with stability.\nThis is meant to reduce the number of UI portrait generation crashes and also eliminate the long battle loading times during normal gameplay.\n \nWe thank the TOW mod team for providing us with this feature. Please check them out on ModDB!", true, true, "Do it", "Not now", new Action(this.BuildShaderCache), new Action(this.HideWindow), "", 0.0f, (Action) null));

    private void BuildShaderCache() => MBGameManager.StartNewGame((MBGameManager) new ShaderGameManager());

    private void HideWindow() => InformationManager.HideInquiry();
  }
}
