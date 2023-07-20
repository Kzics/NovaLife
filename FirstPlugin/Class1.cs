using Life;
using Life.Network;
using System;
using Life.InventorySystem;
using UnityEngine;

namespace FirstPlugin
{
    public class Class1 : Plugin
    {
        public Class1(IGameAPI api): base(api)
        {
        }
        
        public override void OnPluginInit()
        {
            base.OnPluginInit();
            
            SChatCommand cmd = new SChatCommand("/load","permet de retourner la liste de joueurs","/playerlist",(Action<Player, string[]>)((player,arg)=>
            {
                if (player.IsAdmin)
                {
                    if (arg.Length != 1)
                    {
                        player.SendText("Usage corect /load <bundle>");
                        return;
                    }
                    AssetBundleCreateRequest bundleRequest = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + arg[0]);

                    if (bundleRequest.assetBundle != null)
                    {
                        Item item = Nova.server.lifeManager.item.GetItem(1);
                        
                        AssetBundle bundle = bundleRequest.assetBundle;
                        GameObject go = bundle.LoadAsset<GameObject>(bundle.GetAllAssetNames()[0]);

                        if (go != null)
                        {
                            
                            player.SendText("Chargement réussi !");
                            go = GameObject.Instantiate(go);

                            //Position de l'objet
                            go.transform.position = player.setup.transform.position;
                            
                            //Taille de l'objet
                            //go.transform.localScale = item.dropItem.transform.localScale;
                            
                            
                            
                            //A ignorer
                            //item.visual = Nova.server.lifeManager.item.GetItem(10).visual;
                            //item.visual = Nova.server.lifeManager.item.GetItem(10).dropItem;

                            //item.id = 742;
                            
                            player.SendText(go.name);


                            //Nova.server.lifeManager.item.items.Append(item); 
                        }
                        else
                        {
                            player.SendText("L'objet n'a pas pu être chargé depuis l'asset bundle.");
                        }

                        bundle.Unload(false);
                    }
                    else
                    {
                        player.SendText("Erreur lors du chargement de l'asset bundle.");
                    }



                    /*player.SendText("tt");
                    
                    Type type = lifeM.GetType();


                    MethodInfo awakeMethod = type.GetMethod("Awake", BindingFlags.NonPublic | BindingFlags.Instance);

                    if (awakeMethod != null)
                    { 

                        // Appelle la méthode Awake() en utilisant InvokeMember
                        type.InvokeMember("Awake", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, lifeM, null);                    }
                    else
                    {
                        // La méthode Awake() n'a pas été trouvée
                        player.SendText("La méthode Awake() n'a pas été trouvée.");
                    }*/

                    //Nova.server.lifeManager.item.items[0].visual
                    //Item[] obj = Resources.FindObjectsOfTypeAll(typeof(Item)) as Item[];
                    //player.SendText(obj[0].itemName);

                    //Nova.server.lifeManager.item.items = new Item[]{item};

                }else
                {
                    player.SendText("<color=blue> Tu n'es pas admin");
                }
            }));
            
            cmd.Register();
        }
    }
        
}

