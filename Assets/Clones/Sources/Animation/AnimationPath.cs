namespace Clones.Animation
{
    public static class AnimationPath
    {
        public static class UI
        {
            public static class Bool
            {
                public const string IsOpened = nameof(IsOpened);
            }
        }

        public static class Enemy
        {
            public static class Trigger
            {
                public const string Attack = nameof(Attack);
            }

            public static class Bool
            {
                public const string IsMoved = nameof(IsMoved);
            }
        }

        public static class Player
        {
            public static class Trigger
            {
                public const string Attack = nameof(Attack);
            }

            public static class Bool
            {
                public const string IsMoved = nameof(IsMoved);
            }

            public static class Float
            {
                public const string MovementAnimationSpeed = nameof(MovementAnimationSpeed);
                public const string AttackAnimationSpeed = nameof(AttackAnimationSpeed);
            }
        }

        public static class Environment
        {
            public static class Bool
            {
                public const string IdleSpeed = nameof(IdleSpeed);
            }
        }
    }
}
