using System;
using UnityEngine;

namespace AutoPilot
{
	[KSPAddon(KSPAddon.Startup.Flight, false)]
	public class AutoPilotMod : MonoBehaviour
	{
		private APFlightControl flightControl = null;

		/*
         * Called after the scene is loaded. Load resources here.
         */
		public void Awake() {
			Debug.Log("AutoPilot: Awake");
		}

		/*
         * Called next.
         */
		public void Start() {

			Vessel currentVessel = FlightGlobals.ActiveVessel;
			Debug.Log("AutoPilot: Attaching controller...");

			flightControl = new APFlightControl ();
			FlightGlobals.ActiveVessel.OnFlyByWire += flightControl.OnFlyByWire;
			GameEvents.onVesselGoOffRails.Add(flightControl.OnFlightReady);

			APStatusWindow statusWindow = new APStatusWindow (flightControl);
			statusWindow.SetVisible (true);

			Debug.Log("AutoPilot: Started!");
		}

		/*
         * Called every frame
         */
		public void Update()
		{

		}

		/*
         * Called at a fixed time interval determined by the physics time step.
         */
		public void FixedUpdate()
		{

		}
			
		/*
         * Called when the game is leaving the scene (or exiting). Perform any clean up work here.
         */
		public void OnDestroy()
		{
			Debug.Log("AutoPilot Plugin [" + this.GetInstanceID().ToString("X") + "][" + Time.time.ToString("0.0000") + "]: OnDestroy");
		}

		private bool canAttachToVessel(Vessel currentVessel) {
			return currentVessel.isActiveVessel && currentVessel.IsControllable && currentVessel.situation != Vessel.Situations.SPLASHED;
		}
	}

}
	