# Origin

Made for DMA Spring Game Jam 2026. Theme "Falling Apart".

# Repo

https://github.com/Feddas/GameJamDmaSping2026

## Destructible wall setup

- Wall prefabs that should take projectile damage must include both `Health` and `Damageable` components (for example, `Assets/_DarkMoon/Prefabs/DamagableWall.prefab`).
- Ensure wall colliders are on layers included by weapon/projectile `HittableLayers`.
