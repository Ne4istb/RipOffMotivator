cd SmartContracts/Contracts
npx solc --bin --abi Motivator.sol
// copy Motivator.abi and  .bin to project assets

cd ../../
// OPEN IN NEW WINDOW
cd server
 geth --identity "node1" --rpc --rpcport "8000" --rpccorsdomain "*" --datadir "./node1" --port "30303" --nodiscover --rpcapi "db,eth,net,web3,personal,miner,admin" --networkid 1900 --nat "any"

 // build deploySmartContractDeployer.sln

 cd ..
// deploy
// update config
// build RipOffMotivator solution