update `call_channel` set sipserverip='192.168.0.70:5060'
update `call_channel` set domainname='192.168.0.70'
update `call_channel` set sipport='7666'

select * from `call_agent`;

select * from `call_channel`