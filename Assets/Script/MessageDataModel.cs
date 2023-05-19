using System;
using System.Linq;

namespace RocketModel.Models
{
    public struct Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public static Vector3 Zero => new() { X = 0, Y = 0, Z = 0 };

        public UnityEngine.Vector3 Into()
        {
            return new()
            {
                x = Y,
                y = Z,
                z = X
            };
        }

        public override string ToString()
        {
            return $"x:{X} y:{Y} z:{Z}";
        }

        public static bool operator ==(Vector3 v1, Vector3 v2)
        {
            return v1.Equals(v2);
        }

        public static bool operator !=(Vector3 v1, Vector3 v2)
        {
            return !v1.Equals(v2);
        }

#nullable enable
        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;

            var other = (Vector3)obj;
            if (other.X == X && other.Y == Y && other.Z == Z)
                return true;
            return false;
        }
#nullable disable

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }

    public class MessageDataModel
    {
        public float Temperature { get; set; }
        public float Pressure { get; set; }
        public float Altitude { get; set; }
        public Vector3 Acc { get; set; }
        public Vector3 AnguSpeed { get; set; }
        public Vector3 Posture { get; set; }
        public static string NameString => "Temperature,Pressure,Altitude,AccX,AccY,AccZ,AnguSpeedX,AnguSpeedY,AnguSpeedZ,Roll,Pitch,Yaw";
        public static MessageDataModel Zero => new()
        {
            Temperature = 0.0F,
            Altitude = 0.0F,
            Pressure = 0.0F,
            Acc = new Vector3 { X = 0.0F, Y = 0.0F, Z = 0.0F },
            AnguSpeed = new Vector3 { X = 0.0F, Y = 0.0F, Z = 0.0F },
            Posture = new Vector3 { X = 0.0F, Y = 0.0F, Z = 0.0F },
        };

        public override string ToString()
        {
            return $"{Temperature},{Pressure},{Altitude},{Acc},{AnguSpeed},{Posture}";
        }

        public float this[int i]
        {
            get
            {
                return i switch
                {
                    0 => Temperature,
                    1 => Pressure,
                    2 => Altitude,
                    3 => Acc.X,
                    4 => Acc.Y,
                    5 => Acc.Z,
                    6 => AnguSpeed.X,
                    7 => AnguSpeed.Y,
                    8 => AnguSpeed.Z,
                    9 => Posture.X,
                    10 => Posture.Y,
                    11 => Posture.Z,
                    _ => throw new IndexOutOfRangeException(),
                };
            }
        }

        public static bool operator ==(MessageDataModel m1, MessageDataModel m2)
        {
            return m1.Equals(m2);
        }

        public static bool operator !=(MessageDataModel m1, MessageDataModel m2)
        {
            return !m1.Equals(m2);
        }

#nullable enable
        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;

            bool res = true;
            var other = obj as MessageDataModel;
            for (int i = 0; i < 12; i++)
                if (this[i] != other?[i])
                    res &= false;
            return res;
        }
#nullable disable

        public override int GetHashCode()
        {
            return ToString().GetType().GetHashCode();
        }
    }

    public static class MessageDataConverter
    {
        public static MessageDataModel RawMessageToDataConverter(string msg)
        {
            try
            {
                var data = msg.Split(",").Select(d => float.Parse(d)).ToList();
                var RocketData = new MessageDataModel()
                {
                    Temperature = data[0],
                    Pressure = data[1],
                    Altitude = data[2],
                    Acc = new() { X = data[3], Y = data[4], Z = data[5] },
                    AnguSpeed = new() { X = data[6], Y = data[7], Z = data[8] },
                    Posture = new() { X = data[9], Y = data[10], Z = data[11] },
                };
                return RocketData;
            }
            catch
            {
                return MessageDataModel.Zero;
            }
        }
    }
}