using System;

[Flags]
public enum _5A1313F8
{
	Success = 0,
	Corrupted = 1,
	Invalid = 2,
	Blacklisted = 4,
	DateExpired = 8,
	RunningTimeOver = 0x10,
	BadHwid = 0x20,
	MaxBuildExpired = 0x40
}
