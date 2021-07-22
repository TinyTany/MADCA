using MADCA.Core.Data;
using MADCA.Core.Note.Abstract;
using MADCA.Core.Note.Concrete;
using MADCA.UI;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using JsonObject = System.Collections.Generic.Dictionary<string, dynamic>;

namespace MADCA.Utility
{
    public static class MyUtil
    {
        /// <summary>
        /// nullを返すことがあります
        /// </summary>
        /// <param name="lane"></param>
        /// <param name="timing"></param>
        /// <param name="size"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static ShortNote NoteFactory(LanePotision lane, TimingPosition timing, NoteSize size, NoteMode mode)
        {
            switch (mode)
            {
                case NoteMode.Touch:
                    return new Touch(lane, timing, size);
                case NoteMode.Chain:
                    return new Chain(lane, timing, size);
                case NoteMode.SlideL:
                    return new SlideL(lane, timing, size);
                case NoteMode.SlideR:
                    return new SlideR(lane, timing, size);
                case NoteMode.SnapU:
                    return new SnapU(lane, timing, size);
                case NoteMode.SnapD:
                    return new SnapD(lane, timing, size);
                default:
                    //Debug.Assert(false);
                    return null;
            }
        }

        public static void Normalize(JsonObject json)
        {
            foreach(var p in json.ToArray())
            {
                if (p.Value is JsonElement jElem)
                {
                    json[p.Key] = ParsePropertyValue(jElem);
                }
            }

            dynamic ParsePropertyValue(JsonElement elem)
            {
                switch (elem.ValueKind)
                {
                    case JsonValueKind.Array:
                        {
                            return elem.EnumerateArray().Select(e => ParsePropertyValue(e)).ToList();
                        }
                    case JsonValueKind.Object:
                        {
                            return ExpandObject(elem);
                        }
                    default:
                        {
                            return elem.ToString();
                        }
                }
            }

            JsonObject ExpandObject(JsonElement elem)
            {
                return elem.EnumerateObject().Aggregate(new JsonObject(), (acc, cur) => 
                {
                    acc.Add(cur.Name, ParsePropertyValue(cur.Value));
                    return acc;
                });
            }
        }
    }
}
