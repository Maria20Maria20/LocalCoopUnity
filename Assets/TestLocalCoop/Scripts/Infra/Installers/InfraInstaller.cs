using TestLocalCoop.Scripts.Infra.AssetManagement;
using TestLocalCoop.Scripts.Infra.Factories;
using Zenject;
namespace TestLocalCoop.Scripts.Infra.Installers
{
    public class InfraInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindServices();
            BindFactories();
        }
        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
        }
        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<GameStateFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<UIFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<CameraFactory>().AsSingle();
        }
    }
}
