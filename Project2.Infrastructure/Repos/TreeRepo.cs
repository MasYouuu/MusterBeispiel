﻿using AutoMapper;
using Project2.Infrastructure.Context;
using Project2.Infrastructure.DTOs;
using Project2.Infrastructure.Model;
using Project2.Infrastructure.Repos.Interfaces;

namespace Project2.Infrastructure.Repos
{
    public class TreeRepo(GardenContext context, IMapper mapper) : GenericRepo<Tree, TreeDTO>(context, mapper), ITreeRepo
    {
    }
}
