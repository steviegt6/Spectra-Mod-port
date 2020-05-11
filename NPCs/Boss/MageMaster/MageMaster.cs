using Microsoft.Xna.Framework;
using SpectraMod.Projectiles.Boss.MageMaster;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace SpectraMod.NPCs.Boss.MageMaster
{
    [AutoloadBossHead]
    public class MageMaster : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.boss = true;
            npc.aiStyle = -1;
            npc.Size = new Vector2(30, 52);
            npc.lifeMax = 3600;
            npc.defense = 15;
            npc.damage = 4;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.alpha = 85;
            npc.HitSound = SoundID.NPCHit28;
            RageTimes = 77;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            bossLifeScale = 0.5f;
        }

        private const int AI_State_Slot = 0;
        private const int AI_Attack_Time_Slot = 1;
        private const int AI_Timer_Slot = 2;
        private const int AI_Unusued_Slot = 3;

        private bool HalfHealth;
        private bool IsAttacking;
        private bool NotMPClient;
        private int MaxAttacks;
        private int AttackTimes;
        private int AttackDelay;
        private int RageTimes;
        private AttackType NextAttack;

        private const int State_FindTarget = 0;
        private const int State_AttackTarget = 1;
        private const int State_Teleport = 2;
        private const int State_RageAttack = 3;

        public float AI_State { get => npc.ai[AI_State_Slot]; set => npc.ai[AI_State_Slot] = value; }
        public float AI_Attack_Time { get => npc.ai[AI_Attack_Time_Slot]; set => npc.ai[AI_Attack_Time_Slot] = value; }
        public float AI_Timer { get => npc.ai[AI_Timer_Slot]; set => npc.ai[AI_Timer_Slot] = value; }

        private enum AttackType : byte
        {
            Basic,
            Bouncing,
            BasicSpread,
            Shower,
            Homing,
            Circle,
            Null
        }

        public override void AI()
        {
            if (Main.player[npc.target].dead)
            {
                npc.velocity.Y += 1f;
                npc.timeLeft = 10;
            }

            if (Main.expertMode && npc.life < (npc.lifeMax / 3) && RageTimes == 77)
            {
                RageTimes = 0;
                AI_Timer = 0;
                MaxAttacks = 0;
                AttackTimes = 0;
                AttackDelay = 0;
                IsAttacking = true;
                AI_State = State_RageAttack;
            }

            Lighting.AddLight(npc.Center, new Vector3(1.5f, 0f, 1.5f));
            if ((npc.lifeMax < npc.lifeMax / 2)) HalfHealth = true;
            NotMPClient = (Main.netMode != NetmodeID.MultiplayerClient);

            if (AI_State == State_FindTarget)
            {
                npc.TargetClosest(true);
                if (npc.target != -1 || npc.target != 255)
                {
                    AI_Timer = 0;
                    MaxAttacks = 0;
                    AttackTimes = 0;
                    AttackDelay = 0;
                    NextAttack = AttackType.Null;
                    npc.alpha = 85;

                    AI_State = State_AttackTarget;
                }

            }

            if (AI_State == State_AttackTarget)
            {
                Player target = Main.player[npc.target];
                Vector2 dir = npc.DirectionTo(target.Center);
                dir.Normalize();

                if (NotMPClient)
                {
                    AttackType oldATK = NextAttack;
                    while (true)
                    {
                        oldATK = NextAttack;
                        NextAttack = (AttackType)Main.rand.Next(6);
                        if (NextAttack != oldATK) break;
                    }

                    MaxAttacks = 4;
                    AttackDelay = (Main.expertMode ? 80 : 100);

                    AI_Timer++;
                }

                if ((AI_Timer > AttackDelay) && (AttackTimes < MaxAttacks))
                {
                    if (AttackTimes < MaxAttacks)
                    {
                        switch (NextAttack)
                        {
                            case AttackType.Basic:
                                Main.NewText("BasicAtk");
                                Projectile.NewProjectile(npc.Center, dir * 4f, ModContent.ProjectileType<ShadowflareBolt>(), 4, 0.5f);
                                Main.PlaySound(SoundID.Item9);
                                if (NotMPClient) AttackTimes++;
                                break;

                            case AttackType.Bouncing:
                                Main.NewText("BounceAtk");
                                Projectile.NewProjectile(npc.Center, dir * 4f, ModContent.ProjectileType<ShadowflareBounce>(), 4, 0.5f);
                                Main.PlaySound(SoundID.Item9);
                                if (NotMPClient) AttackTimes++;
                                break;

                            case AttackType.BasicSpread:
                                if (NotMPClient) IsAttacking = true;
                                Main.NewText("SpreadAtk");
                                float numberProjectiles = (Main.expertMode ? 4 : 3) + (HalfHealth ? 1 : 0);
                                float rotation = MathHelper.ToRadians(45);
                                Vector2 position = npc.Center;
                                position += Vector2.Normalize(dir) * 45f;
                                for (int i = 0; i < numberProjectiles; i++)
                                {
                                    Vector2 perturbedSpeed = dir.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 2f;
                                    Projectile.NewProjectile(position, perturbedSpeed, ModContent.ProjectileType<ShadowflareBolt>(), 4, 1f);
                                }
                                Main.PlaySound(SoundID.Item9);
                                if (NotMPClient) AttackTimes++;
                                break;

                            case AttackType.Shower:
                                Main.NewText("ShowerAtk");
                                Vector2 loc = target.Center - new Vector2(192, 704);
                                for (int j = 0; j < (HalfHealth ? 7 : 5) + (Main.expertMode ? 2 : 0); j++)
                                {
                                    bool noZeroDiv = j == 0;
                                    Vector2 pos;
                                    if (noZeroDiv) pos = loc;
                                    else pos = loc + new Vector2(64 * j, 0);
                                    Projectile.NewProjectile(pos, new Vector2(0, 4), ModContent.ProjectileType<ShadowflareBolt>(), 4, 1f);
                                }
                                Main.PlaySound(SoundID.Item9);
                                if (NotMPClient) AttackTimes++;
                                break;

                            case AttackType.Homing:
                                Main.NewText("HomingAtk");
                                Projectile.NewProjectile(npc.Center, dir, ModContent.ProjectileType<ShadowflareHome>(), 4, 1f, 255, npc.target);
                                Main.PlaySound(SoundID.Item9);
                                if (NotMPClient) AttackTimes++;
                                break;

                            case AttackType.Circle:
                                Main.NewText("CircleAtk");
                                for (int n = 0; n < 8; n++)
                                {
                                    Vector2 thisDir = dir.RotatedBy(MathHelper.ToRadians(45 * n));
                                    Projectile.NewProjectile(npc.Center, thisDir, ModContent.ProjectileType<ShadowflareBolt>(), 4, 1f);
                                }
                                Main.PlaySound(SoundID.Item9);
                                if (NotMPClient) AttackTimes++;
                                break;

                            default:
                                break;
                        }
                        AI_Timer = 0;
                    }

                    if (AttackTimes >= MaxAttacks)
                    {
                        AI_Timer = 0;
                        MaxAttacks = 0;
                        AttackTimes = 0;
                        AttackDelay = 0;
                        IsAttacking = false;

                        AI_State = State_Teleport;
                    }
                }
            }

            if (AI_State == State_Teleport)
            {
                Vector2 telepos;
                Player target = Main.player[npc.target];
                while (true)
                {
                    telepos = target.Center + new Vector2(Main.rand.Next(-300, 300), Main.rand.Next(-300, 300));
                    Vector2 worldTelepos = telepos;
                    worldTelepos.ToTileCoordinates();
                    Tile teleTile = Framing.GetTileSafely(worldTelepos);
                    if (!teleTile.active())
                    {
                        break;
                    }
                }

                for (int d = 0; d < 15; d++) Dust.NewDust(npc.Center, npc.width, npc.height, DustID.Shadowflame);
                npc.position = telepos;

                AI_State = State_FindTarget;
            }

            if (AI_State == State_RageAttack)
            {
                npc.alpha = 185;
                AI_Timer++;
                if (AI_Timer == 30 && RageTimes < 8)
                {
                    npc.TargetClosest(true);
                    Player target = Main.player[npc.target];
                    Vector2 dir = npc.DirectionTo(target.Center);
                    dir.Normalize();
                    dir *= 5.5f;
                    Projectile.NewProjectile(npc.Center, dir, ModContent.ProjectileType<ShadowflareBolt>(), 4, 1f);
                    Main.PlaySound(SoundID.DD2_EtherianPortalSpawnEnemy, target.position);

                    for (int d2 = 0; d2 < 7; d2++) Dust.NewDust(npc.Center, npc.width, npc.height, DustID.Shadowflame);
                    Vector2 telepos = target.Center + new Vector2(Main.rand.Next(-150, 150), Main.rand.Next(-150, 150));
                    npc.position = telepos;

                    AI_Timer = 0;
                    RageTimes++;
                }

                if (RageTimes == 8)
                {
                    npc.Center = Main.player[npc.target].Center + new Vector2(350, 0);
                    for (int r = 0; r < 3; r++) Dust.NewDust(npc.position, npc.width, npc.height, DustID.WitherLightning);
                    if (AI_Timer == 90)
                    {
                        Projectile.NewProjectile(npc.Center, new Vector2(-6, 0), ModContent.ProjectileType<NeoShadowflare>(), 8, 2f);
                        Main.PlaySound(SoundID.DD2_EtherianPortalDryadTouch);
                        AI_Timer = 0;
                        RageTimes++;
                    }
                }

                if (RageTimes == 9)
                {
                    AI_Timer++;
                    if (AI_Timer == 1)
                    {
                        for (int rd = 0; rd < 15; rd++) Dust.NewDust(npc.position, npc.width, npc.height, DustID.Shadowflame);
                        //npc.position = Main.player[npc.target].position - new Vector2(-200, 200);
                    }    
                    npc.noTileCollide = false;
                    npc.alpha = 10;
                    npc.velocity = new Vector2(0, 4f);
                    if (AI_Timer == 360)
                    {
                        RageTimes++;
                    }
                }

                if (RageTimes == 10)
                {
                    AI_Timer = 0;
                    MaxAttacks = 0;
                    AttackTimes = 0;
                    AttackDelay = 0;
                    IsAttacking = false;
                    npc.alpha = 85;

                    AI_State = State_FindTarget;
                }
            }
        }

        private const int FrameIdle = 0;
        private const int FrameRaised = 1;

        public override void FindFrame(int frameHeight)
        {
            switch (AI_State)
            {
                case State_AttackTarget:
                    npc.frame.Y = FrameRaised * frameHeight;
                    break;
                case State_RageAttack:
                    if (RageTimes == 9) npc.frame.Y = FrameIdle * frameHeight;
                    npc.frame.Y = FrameRaised * frameHeight;
                    break;
                default:
                    npc.frame.Y = FrameIdle * frameHeight;
                    break;
            }
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(IsAttacking);
            writer.WriteVarInt(MaxAttacks);
            writer.WriteVarInt(AttackTimes);
            writer.WriteVarInt(AttackDelay);
            writer.WriteVarInt(RageTimes);
            writer.WriteVarInt((int)NextAttack);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            IsAttacking = reader.ReadBoolean();
            MaxAttacks = reader.ReadInt32();
            AttackTimes = reader.ReadInt32();
            AttackDelay = reader.ReadInt32();
            RageTimes = reader.ReadInt32();
            NextAttack = (AttackType)reader.ReadInt32();
        }
    }
}
