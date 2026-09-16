
delete registry
where id not in
(
select min(id)
from registry
group by acctid, mainkey, subkey)