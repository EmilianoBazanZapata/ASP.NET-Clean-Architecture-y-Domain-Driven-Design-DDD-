﻿using MediatR;

namespace CleanArchitecture.Applicattion.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateSteamerCommand : IRequest
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}
