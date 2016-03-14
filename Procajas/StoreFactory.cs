using Procajas.Store;

namespace Procajas
{
    public static class StoreFactory
    {
        public static IProcajasStore Get(StoreTypes storeType)
        {
            switch (storeType)
            {
                case StoreTypes.Test:
                    return new TestProcajasStore();
                case StoreTypes.ProcajasService:
                default:
                    return new ProcajasServiceStore();
            }
        }

    }

    public enum StoreTypes
    {
        Test,
        ProcajasService
    }
}
