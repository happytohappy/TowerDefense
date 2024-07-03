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

        builder.AddProduct(ruby100, ProductType.Consumable);        // ��ȸ��
        builder.AddProduct(noAds, ProductType.NonConsumable);       // ��ȸ��

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;       
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("�ʱ�ȭ ���� : " + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log("�ʱ�ȭ ���� : " + error + message);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("���� ����");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;

        Debug.Log("���� ���� : " + product.definition.id);

        if (product.definition.id == ruby100)
        {
            Debug.LogError("��� 100�� ���� ����");
        }
        else if (product.definition.id == noAds)
        {
            Debug.LogError("���� ���� ���� ����");
        }

        return PurchaseProcessingResult.Complete;
    }
}
