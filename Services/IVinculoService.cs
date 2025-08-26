namespace ComexApi.Services;

public interface IVinculoService
{
    Task<bool> VincularManifestosEscalas(int escalaId, int manifestoId);
    Task<bool> DesvincularManifestosEscalas(int escalaId, int manifestoId);

}
