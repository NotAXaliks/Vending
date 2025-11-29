using System;
using System.Threading.Tasks;
using VendingDesktop.Dtos;

namespace VendingDesktop.services;

public static class MachinesService
{
    public static async Task<ApiResponse<GetMachinesResponse>> GetMachines(GetMachinesRequest request)
    {
        var machinesResponse = await NetService.Post<GetMachinesResponse>("/machines", request);

        if (machinesResponse.Data == null)
            Console.WriteLine($"Error while fetching POST /machines: {machinesResponse.Error}");

        return machinesResponse;
    }

    public static async Task<ApiResponse<MachineWithModelDto>> CreateMachine(CreateMachineRequest request)
    {
        var machinesResponse = await NetService.Put<MachineWithModelDto>("/machines", request);

        if (machinesResponse.Data == null)
            Console.WriteLine($"Error while fetching PUT /machines: {machinesResponse.Error}");

        return machinesResponse;
    }
}