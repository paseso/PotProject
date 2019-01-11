using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MiniJSON;
using System.IO;

namespace SegaTechBlog
{
    public class ImportAnima2D : EditorWindow
    {

        [MenuItem("Window/Anima2D/Import Anima2D")]
        public static void Open()
        {

            // -----------------------------------------
            // シーンの情報を取得
            // -----------------------------------------
            GameObject meshRoot = GameObject.Find("mesh");
            GameObject boneRoot = GameObject.Find("bone");

            // -----------------------------------------
            // パーツ位置JSONの情報読み出し
            // -----------------------------------------
            string path_select = AssetDatabase.GetAssetPath(Selection.objects[0]);  // 選択されているAssetのパスを取得
            string path_dir = Path.GetDirectoryName(path_select);     // 選択Assetのパスからディレクトリを取り出し
            string path_json = Directory.GetFiles(path_dir, "*-partspos.json")[0];    // 同ディレクトリのパーツ位置 Json を取得
            var jsonText = File.ReadAllText(path_json);
            Dictionary<string, object> jsonData = MiniJSON.Json.Deserialize(jsonText) as Dictionary<string, object>;


            // -----------------------------------------
            // Projectで選択しているSpriteを全て並べる
            // -----------------------------------------
            foreach (Object asset in Selection.objects)
            {

                // -----------------------------------------
                // パーツ位置情報の読み込み
                // -----------------------------------------
                // JSONデータの取り出し(miniJson)
                var jLayer = jsonData[asset.name] as Dictionary<string, object>;
                float index = System.Convert.ToSingle(jLayer["index"]);
                float px = System.Convert.ToSingle(jLayer["x"]);
                float py = System.Convert.ToSingle(jLayer["y"]);

                // -----------------------------------------
                // Anima2Dスプライトの配置
                // -----------------------------------------

                // Mesh
                GameObject tmpMesh = new GameObject(asset.name);
                var smeshInstance = tmpMesh.AddComponent<Anima2D.SpriteMeshInstance>();
                smeshInstance.sharedMaterial = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Sprites-Default.mat");
                tmpMesh.AddComponent<MeshFilter>();
                tmpMesh.AddComponent<MeshRenderer>();

                // Bone
                GameObject tmpBone = new GameObject("j_" + asset.name);
                Anima2D.Bone2D cmpBone = tmpBone.AddComponent<Anima2D.Bone2D>();
                tmpBone.transform.localRotation = Quaternion.Euler(0, 0, 90);

                // Sprite Mesh Instance の設定
                smeshInstance.spriteMesh = asset as Anima2D.SpriteMesh; //  Meshの割り当て
                smeshInstance.bones = new List<Anima2D.Bone2D> { cmpBone };

                // Order in Layer の設定
                smeshInstance.sortingOrder = (int)index;

                // Position の設定
                tmpMesh.transform.position = new Vector3(px / 100.0f, py / 100.0f, 0.0f);
                tmpBone.transform.position = new Vector3(px / 100.0f, py / 100.0f, 0.0f);

                // 親子の設定
                tmpMesh.transform.parent = meshRoot.transform;
                tmpBone.transform.parent = boneRoot.transform;

            }

            // -----------------------------------------
            // Order in layer の値で並べ替え
            // -----------------------------------------
            Dictionary<int, string> dicOrder = new Dictionary<int, string>();
            foreach (Transform child in meshRoot.transform)
            {
                int order = child.GetComponent<Anima2D.SpriteMeshInstance>().sortingOrder;
                dicOrder[order] = child.name;
            }
            ArrayList keys = new ArrayList(dicOrder.Keys);
            keys.Sort();
            keys.Reverse();

            int count = 0;
            foreach (int i in keys)
            {
                meshRoot.transform.Find(dicOrder[i]).SetSiblingIndex(count);
                boneRoot.transform.Find("j_" + dicOrder[i]).SetSiblingIndex(count);
                count++;
            }

        }

    }

}
