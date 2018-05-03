pragma solidity ^0.4.0;

contract Motivator {
    struct Bet {
		string id;
		uint endTime;
        uint deposit;
    }

	mapping(address => Bet[]) bets;


    function getBetIndex(string id) private view returns (uint){ 
        for (uint i = 0; i < bets[msg.sender].length; ++i) {
            if( keccak256( bets[msg.sender][i].id ) == keccak256( id)  ){
                return i;
            } 
        }
        
        require(
            false,
            "invalide access key"
        );
    }
    

    modifier onlyBefore(uint _index) { 
        require(
            now < bets[msg.sender][_index].endTime,
           "outdate time"
        );
        _; 
        
    }
   
    function betting(string uuid, uint endTime) public payable{

	    bets[msg.sender].push(Bet({
	        id: uuid,
	        endTime: endTime,
            deposit: msg.value
        }));

   }

    function reject(string id) 
        public
    {
        Bet memory bet;
        uint index;
        
        index = getBetIndex(id);
        bet = bets[msg.sender][index];
        
        require(
            now < bet.endTime,
           "outdate time"
        );
        
        uint deposit = bet.deposit;
        delete bets[msg.sender][index];
        msg.sender.transfer(deposit);
    }


    

} 