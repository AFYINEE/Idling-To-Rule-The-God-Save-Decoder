using System;

namespace Assets.Scripts.Data
{
	public class LeaderboardScore
	{
		public LeaderBoardType Type;

		public CDouble Score;

		public LeaderboardScore(LeaderBoardType type, CDouble score)
		{
			this.Type = type;
			this.Score = score;
		}
	}
}
