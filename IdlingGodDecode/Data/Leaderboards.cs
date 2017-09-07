using Assets.Scripts.Helper;
using Steamworks;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Data
{
	public class Leaderboards
	{
		public static Leaderboards Instance = new Leaderboards();

		private bool IsSubmittingScore;

		private Stack<LeaderboardScore> ScoresToSubmit = new Stack<LeaderboardScore>();

		private LeaderboardScore CurrentScoreToSubmit;

		private CallResult<LeaderboardFindResult_t> LeaderboardFindResult;

		private SteamLeaderboard_t[] m_SteamLeaderboard = new SteamLeaderboard_t[21];

		public void Init()
		{
			this.LeaderboardFindResult = CallResult<LeaderboardFindResult_t>.Create(new CallResult<LeaderboardFindResult_t>.APIDispatchDelegate(this.OnLeaderboardFindResult));
		}

		public static void SubmitStat(LeaderBoardType type, int score, bool force = false)
		{
			if (App.State.IsBlackListed && !force)
			{
				return;
			}
			if (type.ToString().Contains("Fastest") && score == 0)
			{
				return;
			}
			if (App.CurrentPlattform == Plattform.Steam && App.IsTimeTooOldForSteam)
			{
				return;
			}
			if (App.CurrentPlattform == Plattform.Steam && App.State.Ext.ImportedSaveFromKongToSteam)
			{
				return;
			}
			if ("JaldinSicon".Equals(App.State.SteamName) || "Blas de Lezo".Equals(App.State.SteamName))
			{
				return;
			}
			if ((App.State.ShouldSubmitScore || force) && (force || !App.State.PossibleCheater))
			{
				if (App.CurrentPlattform == Plattform.Steam)
				{
					Leaderboards.Instance.ScoresToSubmit.Push(new LeaderboardScore(type, score));
				}
				else if (App.CurrentPlattform == Plattform.Kongregate)
				{
					Kongregate.SubmitStat(type.ToString(), score, false);
				}
			}
		}

		public void HandleScoreUploads()
		{
			if (this.ScoresToSubmit.Count == 0 || this.IsSubmittingScore || !SteamManager.Initialized)
			{
				return;
			}
			this.CurrentScoreToSubmit = this.ScoresToSubmit.Pop();
			this.IsSubmittingScore = true;
			if (this.m_SteamLeaderboard[(int)this.CurrentScoreToSubmit.Type].m_SteamLeaderboard == 0uL)
			{
				SteamAPICall_t hAPICall = SteamUserStats.FindLeaderboard("L" + this.CurrentScoreToSubmit.Type.ToString());
				this.LeaderboardFindResult.Set(hAPICall, null);
			}
			else
			{
				SteamUserStats.UploadLeaderboardScore(this.m_SteamLeaderboard[(int)this.CurrentScoreToSubmit.Type], ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodForceUpdate, this.CurrentScoreToSubmit.Score.ToInt(), null, 0);
				this.IsSubmittingScore = false;
			}
		}

		public void GetLeaderboardScores()
		{
		}

		private void OnLeaderboardFindResult(LeaderboardFindResult_t pCallback, bool bIOFailure)
		{
			if (pCallback.m_bLeaderboardFound != 0)
			{
				this.m_SteamLeaderboard[(int)this.CurrentScoreToSubmit.Type] = pCallback.m_hSteamLeaderboard;
				SteamUserStats.UploadLeaderboardScore(this.m_SteamLeaderboard[(int)this.CurrentScoreToSubmit.Type], ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodForceUpdate, this.CurrentScoreToSubmit.Score.ToInt(), null, 0);
				this.IsSubmittingScore = false;
			}
		}

		private void OnLeaderboardScoreUploaded(LeaderboardScoreUploaded_t pCallback, bool bIOFailure)
		{
		}
	}
}
