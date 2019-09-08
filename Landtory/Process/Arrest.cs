using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTA;
using NativeFunctionHook;
using Landtory.Engine.API;
using Landtory.Engine.API.Common;

namespace Landtory.Process
{
    class Arrest : Script
    {
        Engine.API.Logger logger = new Engine.API.Logger();
        NPed suspect;
        bool OnArrest;
        bool OnDuty;
        bool ReadyToProceed;
        bool PlayAnim;
        public Arrest()
        {
            logger.Log("Initilazing Arrest", "Arrest");
            this.GUID = new Guid("E8450404-F1B8-4505-A3B6-D77B9C9BF933");
            this.BindScriptCommand("EnableArrest", OnDutyEnabled);
            this.BindScriptCommand("ProceedSuspect", ProceedArrestedSuspect);
            this.Interval = -1;
            this.Tick += new EventHandler(Arrest_Tick);
            this.BindKey(System.Windows.Forms.Keys.E, ArrestSuspect);
        }

        void Arrest_Tick(object sender, EventArgs e)
        {
            if (Exists(Player.GetTargetedPed()) && OnArrest == false)
            {
                Programming.TransferInfo.Render = "Press ~INPUT_PICKUP~ to arrest the suspect when aiming.";
                this.SendScriptCommand("2A0A940D-154B-4513-9FB7-7E12DBB4D8B8", "DrawTargetText");
            }
            if (OnArrest == true && !suspect.GTAPed.Exists() || suspect == null || suspect.IsArrested)
            {
                OnArrest = false;
                return;
            }
            if (suspect != null && suspect.GTAPed.Exists() && ReadyToProceed == true && OnArrest == true && suspect.IsInVehicle() && suspect.GTAPed.CurrentVehicle != Player.Character.CurrentVehicle)
            {
                suspect.GTAPed.Task.LeaveVehicle();
                suspect.GTAPed.RelationshipGroup = RelationshipGroup.Player;
            }
            if (!suspect.GTAPed.isAliveAndWell)
            {
                if(PlayAnim)
                {
                    suspect.GTAPed.Delete();
                    OnArrest = false;
                    PlayAnim = false;
                    ReadyToProceed = false;
                }
            }
        }
        #region Abondoned Code 1
        /*
        void ArrestSuspectRange()
        {
            Ped target = World.GetClosestPed(Player.Character.Position,5);
            try
            {
                logger.Log("Starting Arrest");
                if (target == null)
                {
                    logger.Log("Arrest Failed: Suspect is null", "Arrest", Logger.LogLevel.Error);
                    return;
                }
                if (!Exists(target))
                {
                    logger.Log("Arrest Failed: Suspect does not exist, or not targetting a ped.", "Arrest", Engine.API.Logger.LogLevel.Error);
                    return;
                }
                if (target == suspect.GTAPed)
                {
                    logger.Log("Arrest Failed: Suspect aleardy apprehended", "Arrest", Engine.API.Logger.LogLevel.Error);
                    NGame.PrintSubtitle("~r~This suspect is aleardy apprehended.");
                }
                if (OnArrest == true)
                {
                    logger.Log("Arrest Failed: Alerady arresting", "Arrest", Engine.API.Logger.LogLevel.Error);
                    NGame.PrintSubtitle("~r~You cannot arrest multiple suspect at once.");
                    return;
                }
                if (OnDuty == false)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                NGame.PrintSubtitle(NLanguage.GetLangStr("ArrestException"));
                logger.Log("Exception During Arrest Check: \n" + ex.ToString(), "Arrest", Logger.LogLevel.Error);
                return;
            }
            try
            {
                logger.Log("Arrest Check Passed, starting arrest", "Arrest");
                Random random = new Random();
                int randomed = random.Next(0, 4);
                if (randomed == 1)
                {
                    suspect.GTAPed.Weapons.Glock.Ammo = 5000;
                    suspect.GTAPed.Task.SwapWeapon(Weapon.Handgun_Glock);
                    suspect.GTAPed.Task.FightAgainst(Player.Character, -1);
                }
                suspect = new NPed(target);
                OnArrest = true;
                if (target.isInVehicle())
                {
                    target.Task.LeaveVehicle();
                }
            }
            catch (Exception ex)
            {
                NGame.PrintSubtitle(NLanguage.GetLangStr("ArrestException"));
                logger.Log("Exception During Arrest Init: \n" + ex.ToString(), "Arrest", Logger.LogLevel.Error);
                return;
            }
            if (Exists(Player.LastVehicle))
            {
                try
                {
                    AnimationSet anim = new AnimationSet("busted");
                    AnimationSet anim2 = new AnimationSet("car_bomb");
                    TaskSequence tasks = new TaskSequence();
                    if (anim != null && anim2 != null)
                    {
                        tasks.AddTask.PlayAnimation(anim, "idle_2_hands_up", 1f);
                        tasks.AddTask.PlayAnimation(anim2, "set_car_bomb", 1f);
                        tasks.AddTask.EnterVehicle(World.GetClosestVehicle(Player.Character.Position, 10), VehicleSeat.RightRear);
                        tasks.Perform(target);
                        ReadyToProceed = true;
                        return;
                    }
                    else
                    {
                        tasks.AddTask.HandsUp(5000);
                        tasks.AddTask.EnterVehicle(World.GetClosestVehicle(Player.Character.Position, 10), VehicleSeat.RightRear);
                        tasks.Perform(target);
                        ReadyToProceed = true;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    NGame.PrintSubtitle("FATAL Error during arrest");
                    logger.Log("Arrest suspect error: " + Environment.NewLine + ex.Message + ex.ToString(), "Arrest", Logger.LogLevel.Fatal);
                }
            }
        }
         */
        #endregion
        void ArrestSuspect()
        {
            Ped target = Player.GetTargetedPed();
            try
            {
                logger.Log("Starting Arrest");
                target = Player.GetTargetedPed();
                if(!OnDuty)
                {
                    return;
                }
                if (target == null)
                {
                    logger.Log("Arrest Failed: Null Argument");
                    return;
                }
            }
            catch (NullReferenceException)
            {
                logger.Log("NullReferenceException during Arrest Check");
                target = World.GetClosestPed(Game.LocalPlayer.Character.Position, 5.0f);
            }
            catch (Exception ex)
            {
                NGame.PrintSubtitle(NLanguage.GetLangStr("ArrestException"));
                logger.Log("Exception During Arrest Check - Checking Null: \n" + ex.ToString(), "Arrest", Logger.LogLevel.Error);
                return;
            }
            try
            {
                if (!Exists(target))
                {
                    logger.Log("Arrest Failed: Suspect does not exist, or not targetting a ped.", "Arrest", Engine.API.Logger.LogLevel.Error);
                    return;
                }
                if (suspect != null && target == suspect.GTAPed)
                {
                    logger.Log("Arrest Failed: Suspect aleardy apprehended", "Arrest", Engine.API.Logger.LogLevel.Error);
                    NGame.PrintSubtitle("~r~This suspect is aleardy apprehended.");
                }
            }
            catch(Exception ex)
            {
                NGame.PrintSubtitle(NLanguage.GetLangStr("ArrestException"));
                logger.Log("Exception During Arrest Check - Checking Exists: \n" + ex.ToString(), "Arrest", Logger.LogLevel.Error);
                return;
            }
            try
            {
                if (OnArrest == true)
                {
                    logger.Log("Arrest Failed: Alerady arresting", "Arrest", Engine.API.Logger.LogLevel.Error);
                    NGame.PrintSubtitle(NLanguage.GetLangStr("MultipleSuspectArrestAttmept"));
                    return;
                }
                if (OnDuty == false)
                {
                    return;
                }
            }
            catch(Exception ex)
            {
                NGame.PrintSubtitle(NLanguage.GetLangStr("ArrestException"));
                logger.Log("Exception During Arrest Check - Checking Status: \n" + ex.ToString(), "Arrest", Logger.LogLevel.Error);
                return;
            }
            
            Random random = new Random();
            int randomed = random.Next(0, 4);
            
                logger.Log("Arrest Check Passed, starting arrest", "Arrest");
                
                
            
            try
            {
                if (randomed == 1)
                {
                    suspect.GTAPed.Weapons.Glock.Ammo = 5000;
                    suspect.GTAPed.Task.SwapWeapon(Weapon.Handgun_Glock);
                    suspect.GTAPed.Task.FightAgainst(Player.Character, -1);
                }
                suspect = new NPed(target);
                OnArrest = true;
                if (target.isInVehicle())
                {
                    target.Task.LeaveVehicle();
                }
            }
            catch (Exception ex)
            {
                NGame.PrintSubtitle(NLanguage.GetLangStr("ArrestException"));
                logger.Log("Exception During Arrest Init: \n" + ex.ToString(), "Arrest", Logger.LogLevel.Error);
                return;
            }
            target = Player.GetTargetedPed();

            if (Exists(Player.LastVehicle))
            {
                try
                {
                    AnimationSet anim = new AnimationSet("busted");
                    AnimationSet anim2 = new AnimationSet("car_bomb");
                    TaskSequence tasks = new TaskSequence();
                    if (anim != null && anim2 != null)
                    {
                        tasks.AddTask.PlayAnimation(anim, "idle_2_hands_up", 1f);
                        tasks.AddTask.PlayAnimation(anim2, "set_car_bomb", 1f);
                        tasks.AddTask.Die();
                        tasks.Perform(target);
                        PlayAnim = true;
                        ReadyToProceed = true;
                        return;
                    }
                    else
                    {
                        tasks.AddTask.HandsUp(5000);
                        tasks.AddTask.EnterVehicle(World.GetClosestVehicle(Player.Character.Position, 10), VehicleSeat.RightRear);
                        tasks.Perform(target);
                        ReadyToProceed = true;
                        return;
                    }
                }
                catch(Exception ex)
                {
                    NGame.PrintSubtitle("FATAL Error during arrest");
                    logger.Log("Arrest suspect error: " + Environment.NewLine + ex.Message + ex.ToString(), "Arrest", Logger.LogLevel.Fatal);
                }
            }
        }
        void OnDutyEnabled(Script sender, ObjectCollection parameter)
        {
            logger.Log("Arrest enabled signal received", "Arrest");
            OnDuty = true;
            this.Interval = 100;
        }
        void ProceedArrestedSuspect(Script sender, ObjectCollection parameter)
        {
            try
            {
                if (suspect == null)
                {
                    logger.Log("Proceed arrest signal invaild", "Arrest", Logger.LogLevel.Warning);
                    return;
                }
                logger.Log("Proceed arrested suspect signal received", "Arrest");
                if (OnArrest = false || !Exists(suspect) || PlayAnim == true || !suspect.GTAPed.isAliveAndWell)
                {
                    return;
                }
                OnArrest = false;
                if (suspect.IsInVehicle())
                {
                    suspect.GTAPed.Task.LeaveVehicle();
                }

                for (int num11 = 51; num11 > 1; num11--)
                {
                    Functions.SetPedAlpha(suspect.GTAPed, num11 * 5);
                    Game.WaitInCurrentScript(10);
                }
                logger.Log("Deleting suspect", "Arrest");
                Functions.SetPedAlpha(suspect.GTAPed, 0);
                suspect.GTAPed.NoLongerNeeded();
                suspect.GTAPed.Delete();
                suspect = null;
                logger.Log("Suspect Apprehended", "Arrest");
                NGame.PrintSubtitle(NLanguage.GetLangStr("Apprehended"));
            }
            catch(NullReferenceException)
            {
                logger.Log("Null Reference Exception reported","Arrest", Logger.LogLevel.Fatal);
            }
            catch(NonExistingObjectException)
            {
                logger.Log("Non Existing Object Exception reported","Arrest");
            }
        }
    }
}
