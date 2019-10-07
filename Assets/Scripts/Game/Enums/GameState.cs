namespace LDJam45.Game
{
	public enum GameState
	{
		None,
		Intro,
		InitializeGame,
		InitializeFinished,
		Movable,
		MoveToRoom,
		MoveToRoomFinished,
		BattleBegin,
		BattleFinished,
		PlayerTurnStart,
		PlayerTurnEnd,
		EnemyTurnStart,
		EnemyTurnEnd,
		GameOver
	}
}