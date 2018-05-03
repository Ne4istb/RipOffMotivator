pragma solidity ^0.4.0;

contract Motivator {
    struct Bet {
		uint endTime;
        uint deposit;
		bool open;
    }

	mapping(address => Bet[]) bets;


    modifier valideIndex(uint _index) { 
        require(
            _index < bets[msg.sender].length,
            "invalide access key"
        );
        _; 
        
    }

    modifier onlyBefore(uint _index) { 
        require(
            now < bets[msg.sender][_index].endTime,
           "outdate time"
        );
        _; 
        
    }
   
    function betting(uint endTime) public payable returns (uint){
        uint length = bets[msg.sender].length;

	    bets[msg.sender].push(Bet({
	        endTime: endTime,
            deposit: msg.value,
            open: true
        }));
        
        return length;
   }

    function reject(uint index) 
        public
        valideIndex(index)
        onlyBefore(index)
    {
        Bet bet = bets[msg.sender][index];
        bet.open = false;
        msg.sender.transfer(bet.deposit);
    }


    

} 