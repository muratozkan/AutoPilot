using System;
using UnityEngine;
using AutoPilot.UI;
using AutoPilot.Control;

namespace AutoPilot
{
	[KSPAddon(KSPAddon.Startup.Flight, false)]
	public class AutoPilotMod : MonoBehaviour
	{
		private APSimpleControl flightControl = new APSimpleControl ();

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

			FlightGlobals.ActiveVessel.OnFlyByWire += OnFlyByWire;
			GameEvents.onVesselGoOffRails.Add(OnFlightReady);

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

		private void OnFlightReady(Vessel vessel)
		{
			Debug.Log ("AutoPilot: OnFlightReady");
		}

		private void OnFlyByWire(FlightCtrlState state) 
		{
			Vessel vessel = FlightGlobals.ActiveVessel;

			APFlightData data = new APFlightData {
				vHorizontal = (float) vessel.horizontalSrfSpeed,
				vVertical = (float) vessel.verticalSpeed,
				velocity = vessel.GetSrfVelocity (),
				rotation = vessel.srfRelRotation
			};

			APTarget target = new APTarget {
				altitude = 1000f
			};

			flightControl.FlightData = data;
			flightControl.Target = target;

			flightControl.Update ((float) HighLogic.CurrentGame.UniversalTime);

			state.pitch += flightControl.Command.pitch;
		}
	}
}
	