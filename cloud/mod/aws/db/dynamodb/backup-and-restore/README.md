# Backup and Restore

## On-Demand Backup and Restore

> * Full backups at any time
>
> * Zero impact on table performance or availability
>
> * Consistent within seconds and **retained until deleted**
>
> * Operates within same region as the source table

* So you can't perform backups and restores across regions

## Point-in-Time Recovery (PITR)

> * Protects against accidental writes or deletes
>
> * Restore to any point in the last **35 days**
>
> * Incremental backups
>
> * Not enabled by default
>
> * Latest restorable: **five minutes** in the past
