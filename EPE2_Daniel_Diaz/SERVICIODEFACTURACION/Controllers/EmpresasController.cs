using Microsoft.AspNetCore.Mvc;
using SERVICIODEFACTURACION.Modelos;

namespace SERVICIODEFACTURACION.Controllers
{

    [ApiController]
    public class EmpresasController : ControllerBase
    {
        // Arreglo que almacena las empresas
        public Empresa[] _empresa = new Empresa[]
        {
            // Inicialización de datos de ejemplo
            new Empresa 
            { RutEmpresa = "55532123k",
              NombreEmpresa = "Invenciones sa",
              GiroEmpresa="Confecciones de ideas",
              RutCliente="18599175k",
              NombreCliente="Daniel",
              ApellidosCliente="Diaz Leiva",
              EdadCliente=30, MontoVentas=300,
              TotalVentas=3000000,
              MontoIVAApagar=570000, 
              MontoUtilidades=2430000
            },

            new Empresa 
            { RutEmpresa = "500534863", 
              NombreEmpresa = "Banco Invenciones sa", 
              GiroEmpresa="Resguardo de ideas",
              RutCliente="114800090",
              NombreCliente="Monica",
              ApellidosCliente="Quezada Solis",
              EdadCliente=57, MontoVentas=1000, 
              TotalVentas=5000000, 
              MontoIVAApagar=950000, 
              MontoUtilidades=4050000
            },

            new Empresa 
            { RutEmpresa = "331212240",
              NombreEmpresa = "Logistica Invenciones ltda", 
              GiroEmpresa="Transporte de ideas",
              RutCliente="97656126",
              NombreCliente="Patricio",
              ApellidosCliente="Jimenez Moya",
              EdadCliente=61, MontoVentas=1600, 
              TotalVentas=4000000, 
              MontoIVAApagar=760000, 
              MontoUtilidades=3240000
            },

            new Empresa 
            { RutEmpresa = "404404404", 
              NombreEmpresa = "Dummys y asociados", 
              GiroEmpresa="Almacenaje",
              RutCliente="185975310",
              NombreCliente="Alejandro",
              ApellidosCliente="Escudero Perez",
              EdadCliente=29, 
              MontoVentas=200, 
              TotalVentas=2000000, 
              MontoIVAApagar=380000, MontoUtilidades=1620000
            }

        };

        // Método GET para listar las primeras tres empresas
        [HttpGet]
        [Route("Empresa")]
        public IActionResult ListarTresEmpresas()
        {
            try
            {
                //Verificar que el arreglo no nulo y si tiene al menos 3 elementos
                if (_empresa != null && _empresa.Length >= 3)
                {
                    // Crea una lista que contenga solo los primeros tres registros
                    var primerosTresClientes = _empresa.Take(3).ToArray();

                    return StatusCode(200, primerosTresClientes);
                }
                else
                {
                    return StatusCode(404, "No se encontraron los datos o no hay suficientes registros");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno");
            }
        }

        // Método GET para listar empresas por RutEmpresa
        [HttpGet]
        [Route("Empresa/{RutEmpresa}")]
        public IActionResult ListarEmpresaPorRut(string RutEmpresa)
        {
            try
            {
                if (_empresa != null)
                {
                    //Incialización arreglo que guardará datos filtrados
                    Empresa[] empresasFiltrados = new Empresa[_empresa.Length];
                    int count = 0;

                    //Recorre el arreglo y luego verifica que el valor proporcionado coincida con alguno de los almacenados
                    for (int i = 0; i < _empresa.Length; i++)
                    {
                        if (_empresa[i].RutEmpresa == RutEmpresa)
                        {
                            empresasFiltrados[count] = _empresa[i];
                            count++;
                        }
                    }

                    if (count > 0)
                    {
                        // Ajustar el tamaño del arreglo al número real de elementos
                        Array.Resize(ref empresasFiltrados, count); 
                        return StatusCode(200, empresasFiltrados);
                    }
                    else
                    {
                        return StatusCode(404, "No se encontraron clientes con el RutEmpresa especificado");
                    }
                }
                else
                {
                    return StatusCode(404, "No se encontraron los datos");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno");
            }
        }
        // Método POST para crear una nueva empresa
        [HttpPost]
        [Route("Empresa")]
        public IActionResult CrearNuevaEmpresa([FromBody] Empresa nuevaEmpresa)
        {
            try
            {
                //Verificar la validez de los datos de una nueva empresa
                if (nuevaEmpresa != null && !string.IsNullOrEmpty(nuevaEmpresa.RutEmpresa) && !string.IsNullOrEmpty(nuevaEmpresa.NombreEmpresa))
                {
                    bool existeEmpresaConMismoRut = false;

                    for (int i = 0; i < _empresa.Length; i++)
                    {
                        //Verificar la existencia de la empresa
                        if (_empresa[i].RutEmpresa == nuevaEmpresa.RutEmpresa)
                        {
                            existeEmpresaConMismoRut = true;
                            break;
                        }
                    }

                    if (existeEmpresaConMismoRut)
                    {
                        return StatusCode(409, "Ya existe una empresa con el mismo RutEmpresa");
                    }

                    // Agrega la nueva empresa a la matriz
                    Array.Resize(ref _empresa, _empresa.Length + 1);
                    _empresa[_empresa.Length - 1] = nuevaEmpresa;

                    return StatusCode(201, nuevaEmpresa);
                }
                else
                {
                    return StatusCode(400, "Los datos proporcionados son inválidos");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno");
            }
        }

        // Método PUT para editar una empresa existente
        [HttpPut]
        [Route("Empresa/{RutEmpresa}")]
        public IActionResult EditarEmpresa(string RutEmpresa, [FromBody] Empresa empresaActualizada)
        {
            try
            {
                if (_empresa != null)
                {
                    // Buscar la empresa por su RutEmpresa
                    var empresaExistente = _empresa.FirstOrDefault(empresa => empresa.RutEmpresa == RutEmpresa);

                    if (empresaExistente != null)
                    {
                        // Actualizar los datos de la empresa con los nuevos datos proporcionados
                        empresaExistente.NombreEmpresa = empresaActualizada.NombreEmpresa;
                        empresaExistente.GiroEmpresa = empresaActualizada.GiroEmpresa;
                        empresaExistente.RutCliente = empresaActualizada.RutCliente;
                        empresaExistente.NombreCliente = empresaActualizada.NombreCliente;
                        empresaExistente.ApellidosCliente = empresaActualizada.ApellidosCliente;
                        empresaExistente.EdadCliente = empresaActualizada.EdadCliente;
                        empresaExistente.MontoVentas = empresaActualizada.MontoVentas;
                        empresaExistente.TotalVentas = empresaActualizada.TotalVentas;
                        empresaExistente.MontoIVAApagar = empresaActualizada.MontoIVAApagar;
                        empresaExistente.MontoUtilidades = empresaActualizada.MontoUtilidades;

                        return StatusCode(200, empresaExistente);
                    }
                    else
                    {
                        return StatusCode(404, "No se encontró una empresa con el RutEmpresa especificado");
                    }
                }
                else
                {
                    return StatusCode(404, "No se encontraron los datos");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno");
            }
        }

        //Método DELETE para borrar una emrpesa existente 
        [HttpDelete]
        [Route("Empresa/{RutEmpresa}")]
        public IActionResult EliminarEmpresa(string RutEmpresa)
        {
            try
            {
                if (_empresa != null)
                {
                    //el '-1' indica que inicialmente no se ha encontrado una empresa para eliminar
                    int indiceEliminar = -1;

                    // Buscar la empresa por su RutEmpresa
                    for (int i = 0; i < _empresa.Length; i++)
                    {
                        if (_empresa[i].RutEmpresa == RutEmpresa)
                        {
                            indiceEliminar = i;
                            break;
                        }
                    }
                    
                    if (indiceEliminar != -1)
                    {
                        // Eliminar la empresa encontrada
                        var empresaEliminada = _empresa[indiceEliminar];

                        if (_empresa.Length == 1)
                        {
                            // Si es el único elemento, simplemente vacía el arreglo
                            _empresa = new Empresa[0];
                        }
                        else
                        {
                            // Reajusta el tamaño del arreglo excluyendo la empresa eliminada
                            var nuevasEmpresas = new Empresa[_empresa.Length - 1];
                            for (int i = 0, j = 0; i < _empresa.Length; i++)
                            {
                                if (i != indiceEliminar)
                                {
                                    nuevasEmpresas[j] = _empresa[i];
                                    j++;
                                }
                            }
                            _empresa = nuevasEmpresas;
                        }

                        return StatusCode(200, empresaEliminada);
                    }
                    else
                    {
                        return StatusCode(404, "No se encontró una empresa con el RutEmpresa especificado");
                    }
                }
                else
                {
                    return StatusCode(404, "No se encontraron los datos");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno");
            }
        }





    }

}

        

