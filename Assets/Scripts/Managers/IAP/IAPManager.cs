using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    public IStoreController storeController;

    public string ruby100 = "ruby_100";
    public string noAds = "noads";

    public void Init()
    {
        InitIAP();
    }

    private void InitIAP()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(ruby100, ProductType.Consumable);        // 다회성
        builder.AddProduct(noAds, ProductType.NonConsumable);       // 일회성

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;       
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("초기화 실패 : " + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log("초기화 실패 : " + error + message);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("구매 실패");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;

        Debug.Log("구매 성공 : " + product.definition.id);

        if (product.definition.id == ruby100)
        {
            Debug.LogError("루비 100개 구매 성공");
        }
        else if (product.definition.id == noAds)
        {
            Debug.LogError("광고 제거 구매 성공");
        }

        return PurchaseProcessingResult.Complete;
    }
}
