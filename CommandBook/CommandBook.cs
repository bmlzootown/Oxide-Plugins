using System.Collections.Generic;
using System.Collections;
using System;
using System.Reflection;
using System.Data;
using UnityEngine;
using Oxide.Core;

namespace Oxide.Plugins {
	
	[Info("CommandBook", "bmlee2", "0.0.1")]
	class CommandBook : RustPlugin {
		
		[ChatCommand("pm")]
		void cmdChatPM(BasePlayer player, string command, string[] args) {
			if (args.Length > 0) {
				List<string> pm = new List<string>(args);
				string SendTo = pm[0];
				
				var playz = new List<string>();
				var Online = BasePlayer.activePlayerList as List<BasePlayer>;
				foreach(BasePlayer playerz in Online) {
					playz.Add(playerz.displayName);
				}
				
				if (playz.Contains(SendTo)) {
					var SendzTo = BasePlayer.Find(SendTo);
					pm.RemoveAt(0);
						if (pm.Count >= 1) {
							var message = String.Join(" ", pm.ToArray());
							SendReply(player, "To " + SendTo + ": " + message);
							SendReply(SendzTo, "From " + player.displayName + ": " + message);
						} else {
							SendReply(player, "Error: Uh, you done forgot da message, silly!");
						}
				} else {
					SendReply(player, "Player is not currently online.");
				}
				
			} else {
				SendReply(player, "Format: /pm [player] [message]");
			}
		}
		
		[ChatCommand("who")]
		void cmdChatWho(BasePlayer player, string command, string[] args) {
			var playz = new List<string>();
			var Online = BasePlayer.activePlayerList as List<BasePlayer>;
			foreach(BasePlayer playerz in Online) {
				playz.Add(playerz.displayName);
			}
			var PlayerList = String.Join(", ", playz.ToArray());
			SendReply(player, playz.Count + " player(s) online:");
			SendReply(player, PlayerList);
		}
		
		[ChatCommand("broadcast")]
		void cmdChatBroadcast(BasePlayer player, string command, string[] args) {
			if (player.net.connection.authLevel >= 1) {
				if (args.Length > 0) {
					var message = String.Join(" ", args);
					PrintToChat(message);
				} else {
					SendReply(player, "Error: Uh, you're supposed to include a message, no? <.<");
				}
			} else {
				SendReply(player, "Error: You do not have permission to use this command!");
			}
		}
	}
}